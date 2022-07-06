using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CourseJournal.Infrastructure
{
    public class CoursesRepository : ICoursesRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CourseJournalDbConnectionString"].ConnectionString;

        public async Task<bool> CreateNewAsync(Course newCourse)
        {
            SqlTransaction transaction;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    transaction = connection.BeginTransaction();

                    var createNewCommandText = $@"
                        INSERT INTO [Courses]
                        ([Name], [StartDate], [Trainer], [PresenceThreshold], [HwResultsThreshold], [WtResultsThreshold])
                        VALUES (@Name, @StartDate, @TrainerId, @PresenceThreshold, @HwResultsThreshold, @WtResultsThreshold)
                        ;";

                    var createNewCommandSql = new SqlCommand(createNewCommandText, connection, transaction);
                    createNewCommandSql.Parameters.Add("@Name", SqlDbType.VarChar, 255).Value = newCourse.Name;
                    createNewCommandSql.Parameters.Add("@StartDate", SqlDbType.DateTime2).Value = newCourse.StartDate;
                    createNewCommandSql.Parameters.Add("@TrainerId", SqlDbType.Int).Value = newCourse.TrainerId;
                    createNewCommandSql.Parameters.Add("@PresenceThreshold", SqlDbType.Int).Value = newCourse.PresenceThreshold;
                    createNewCommandSql.Parameters.Add("@HwResultsThreshold", SqlDbType.Int).Value = newCourse.HwResultsThreshold;
                    createNewCommandSql.Parameters.Add("@WtResultsThreshold", SqlDbType.Int).Value = newCourse.WtResultsThreshold;
                    await createNewCommandSql.ExecuteNonQueryAsync();

                    string getLastCourseIdCommandText = $"SELECT IDENT_CURRENT('Courses') AS [CourseId];";
                    var getLastCourseIdCommandSql = new SqlCommand(getLastCourseIdCommandText, connection, transaction);

                    var reader = await getLastCourseIdCommandSql.ExecuteReaderAsync();
                    await reader.ReadAsync();

                    int courseId = int.Parse(reader["CourseId"].ToString());
                    reader.Close();

                    string addCourseStudentsCommandText = @"
                        INSERT INTO [CourseStudents] ([CourseId], [StudentId])
                        VALUES (@CourseId, @StudentId
                        )";

                    foreach (var student in newCourse.Students)
                    {
                        var addCourseStudentsCommandSql = new SqlCommand(addCourseStudentsCommandText, connection, transaction);
                        addCourseStudentsCommandSql.Parameters.Add("@CourseId", SqlDbType.Int).Value = courseId;
                        addCourseStudentsCommandSql.Parameters.Add("@StudentId", SqlDbType.Int).Value = student.Id;

                        await addCourseStudentsCommandSql.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return false;
            }

            return true;
        }
    }
}