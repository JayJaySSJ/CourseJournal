using CourseJournal.AdminApp.Domain.Models;
using CourseJournal.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CourseJournal.Infrastructure
{
    public class TrainersRepository : ITrainersRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["CourseJournalDbConnectionString"].ConnectionString;

        public async Task<bool> CreateTrainer(Trainer trainer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string commandSql = "INSERT INTO [Trainers] ([Name], [Surname], [Email], [Password], [BirthDate]) VALUES (@Name, @Surname, @Email, @Password, @BirthDate)";
                    SqlCommand command = new SqlCommand(commandSql, connection);

                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 255).Value = trainer.Name;
                    command.Parameters.Add("@Surname", SqlDbType.NVarChar, 255).Value = trainer.Surname;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = trainer.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 255).Value = trainer.Password;
                    command.Parameters.Add("@BirthDate", SqlDbType.DateTime2).Value = trainer.BirthDate;

                    if (await command.ExecuteNonQueryAsync() == 1)
                    {
                        return true;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        public async Task<Trainer> GetTrainer(string email)
        {
            Trainer trainer = null;

            try
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = "SELECT * FROM [Trainers] WHERE [Email] = @Email";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 255).Value = email;

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();
                    await dataReader.ReadAsync();

                    if(dataReader.HasRows)
                    {
                        trainer = new Trainer
                        {
                            Id = int.Parse(dataReader["Id"].ToString()),
                            Name = dataReader["Name"].ToString(),   
                            Surname = dataReader["Surname"].ToString(),
                            Email = email,
                            Password = dataReader["Password"].ToString(),
                            BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())
                        };

                        return trainer;
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                trainer = null;
            }

            return trainer;
        }

        public async Task<Trainer> GetTrainerById(int id)
        {
            Trainer trainer = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string commandText = $"SELECT * FROM [Trainers] WHERE [Id] = @Id";

                    SqlCommand command = new SqlCommand(commandText, connection);
                    command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    SqlDataReader dataReader = await command.ExecuteReaderAsync();

                    await dataReader.ReadAsync();

                    if (dataReader.HasRows)
                    {
                        trainer = new Trainer
                        {
                            Id = id,
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Password = dataReader["Password"].ToString(),
                            BirthDate = DateTime.Parse(dataReader["BirthDate"].ToString())
                        };

                        return trainer;
                    }

                    connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                trainer = null;
            }

            return trainer;
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var getAllCommandText = "SELECT * FROM [Trainers]";

                    var getAllCommandSql = new SqlCommand(getAllCommandText, connection);
                    var reader = await getAllCommandSql.ExecuteReaderAsync();

                    var trainers = new List<Trainer>();

                    while (await reader.ReadAsync())
                    {
                        var trainer = new Trainer
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                            Surname = reader["Surname"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            BirthDate = DateTime.Parse(reader["BirthDate"].ToString())
                        };

                        trainers.Add(trainer);
                    }

                    return trainers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                return new List<Trainer>();
            }
        }
    }
}
