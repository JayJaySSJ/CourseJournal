using CourseJournal.Domain.Interfaces;
using CourseJournal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CourseJournal.Infrastructure
{
    public class HomeworksResultsRepository: IHomeworksResultsRpository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CourseJournalDbConnectionString"].ConnectionString;

        public async Task<bool> CreateHomeworkResult(HomeworkResult homeworkResult)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [HomeworksResults] ([NameHomework], [ReturnDate], [Result], [StudentId], [CourseId]) VALUES (@NameHomework, @ReturnDate, @Result, @StudentId, @CourseId)";
                    SqlCommand command = new SqlCommand(commandSql, connection);

                    command.Parameters.Add("@NameHomework", SqlDbType.NVarChar, 255).Value = homeworkResult.NameHomework;
                    command.Parameters.Add("@ReturnDate", SqlDbType.DateTime2).Value = homeworkResult.ReturnDate;
                    command.Parameters.Add("@Result", SqlDbType.Decimal).Value = homeworkResult.Result;
                    command.Parameters.Add("@StudentId", SqlDbType.NVarChar, 255).Value = homeworkResult.StudentId;
                    command.Parameters.Add("@CourseId", SqlDbType.NVarChar, 255).Value = homeworkResult.CourseId;
                   

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}
