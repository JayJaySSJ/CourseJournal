using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
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

        public async Task<List<Course>> GetAllAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var getAllCommandText = "SELECT * FROM [Courses]";

                    var getAllCommandSql = new SqlCommand(getAllCommandText, connection);
                    var reader = await getAllCommandSql.ExecuteReaderAsync();

                    var courses = new List<Course>();

                    while (await reader.ReadAsync())
                    {
                        var course = new Course
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            TrainerId = int.Parse(reader["Trainer"].ToString()),
                            StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                            Students = new List<Student>(),                             // TODO: FINISH IT !!!
                            HwResultsThreshold = int.Parse(reader["HwResultsThreshold"].ToString()),
                            PresenceThreshold = int.Parse(reader["PresenceThreshold"].ToString()),
                            WtResultsThreshold = int.Parse(reader["WtResultsThreshold"].ToString())
                        };

                        courses.Add(course);
                    }

                    return courses;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return new List<Course>();
            }
        }
    }
}