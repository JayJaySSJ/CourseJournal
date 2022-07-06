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
    public class StudentsRepository:IStudentsRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["CourseJournalDbConnectionString"].ConnectionString;

        public async Task<bool> AddAsync(Student student)
        {
            bool success;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"INSERT INTO [Students] ([Name],[Surname],[Email],[Password],[BirthDate]) VALUES ('{student.Name}','{student.Surname}','{student.Email}','{student.Password}', '{student.BirthDate}' )";
                    SqlCommand command = new SqlCommand(commandText, connection);
                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    success = rowsAffected == 1;

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        public async Task<List<Student>> GetAllAsync()
        {
           
                List<Student> students = new List<Student>();

                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();

                        string commandText = $"SELECT * FROM [Students]";
                        SqlCommand command = new SqlCommand(commandText, connection);
                        SqlDataReader dataReader = await command.ExecuteReaderAsync();

                        while (await dataReader.ReadAsync())
                        {
                            Student student;

                            try
                            {
                            student = new Student()
                            {
                                Id = int.Parse(dataReader["Id"].ToString()),
                                Name = dataReader["Name"].ToString(),
                                Surname = dataReader["Surname"].ToString(),
                                Email = dataReader["Email"].ToString(),
                                BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())                                                                            
                               
                            };
                        }
                            catch (Exception)
                            {
                                continue;
                            }

                            students.Add(student);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    students = new List<Student>();
                }

                return students;
            
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var getStudentByIdCommandText = "SELECT * FROM [Students] WHERE [Id] = @Id";

                    var getStudentByIdCommandSql = new SqlCommand(getStudentByIdCommandText, connection);
                    getStudentByIdCommandSql.Parameters.Add("@Id", SqlDbType.VarChar, 255).Value = id;

                    var reader = await getStudentByIdCommandSql.ExecuteReaderAsync();
                    await reader.ReadAsync();

                    return new Student
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Surname = reader["Surname"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        BirthDate = DateTime.Parse(reader["BirthDate"].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Student();
            }
        }
    }
}
