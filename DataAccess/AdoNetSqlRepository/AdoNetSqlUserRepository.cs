using System;
using  System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using DataAccess.EntityFrameworkRepository;

namespace DataAccess.AdoNetSqlRepository
{
    public class AdoNetSqlUserRepository : IRepository<User>
    {
        private readonly string SQL_CONNECTION_STRING =
            ConfigurationManager.ConnectionStrings["AppConnection"].ConnectionString;

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            //Using EntityConnection and SqlCommand
            //Getting SqlConnection from EntityConnection
            EntityConnection entityConnection = new EntityConnection("name=AppEntities");
            using (var connection = (SqlConnection) entityConnection.StoreConnection)
            {

                connection.Open();
                string getCommand = @"SELECT * FROM Users";
                using (var sqlCommand = new SqlCommand(getCommand, connection))
                {
                    var sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (sqlDataReader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = sqlDataReader.GetInt32(0),
                            FirstName = sqlDataReader["FirstName"].ToString(),
                            LastName = sqlDataReader["LastName"].ToString(),
                            City = sqlDataReader["City"].ToString(),
                            Username = sqlDataReader["Username"].ToString()

                        });
                    }
                }
            }
            return users;
        }

        public User GetById(int id)
        {
            User user = new User();

            using (var sqlConnection = new SqlConnection(SQL_CONNECTION_STRING))
            {
                sqlConnection.Open();

                string getByIdCommand = "SELECT * FROM USERS U WHERE U.Id = @id";
                using (var sqlCommand = new SqlCommand(getByIdCommand, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Id", id));
                    using(var sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (sqlDataReader.Read())
                        {
                            user =  new User()
                            {
                                Id = sqlDataReader.GetInt32(0),
                                FirstName = sqlDataReader.GetString(1),
                                LastName = sqlDataReader.GetString(2),
                                Username = sqlDataReader["Username"].ToString(),
                                City = sqlDataReader.GetValue(4).ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

        public void Add(User entity)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                using (var sqlConnection = new SqlConnection(SQL_CONNECTION_STRING))
                {
                    sqlConnection.Open();
                    string insertCommand = "INSERT INTO USERS(FirstName, LastName, Username, City) " +
                                           "VALUES (@firstName, @lastName, @userName, @city)";
                    using (var sqlCommand = new SqlCommand(insertCommand, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(new SqlParameter("firstName", entity.FirstName));
                        sqlCommand.Parameters.Add(new SqlParameter("lastName", entity.LastName));
                        sqlCommand.Parameters.Add(new SqlParameter("username", entity.Username));
                        sqlCommand.Parameters.Add(new SqlParameter("city", entity.City));

                        sqlCommand.ExecuteNonQuery(); //returns rows affected
                    }
                }

                transactionScope.Complete();
            }
        }

        public void Update(User entity)
        {
            throw new System.NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
