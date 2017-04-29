using System;
using  System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using DataAccess.EntityFrameworkRepository;

namespace DataAccess.AdoNetSqlRepository
{
    public class AdoNetSqlUserRepository : IRepository<User>
    {

        public IEnumerable<User> GetAll()
        {
            List<User> users = new List<User>();
            //Using EntityConnection and SqlCommand
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
            throw new NotImplementedException();
        }

        public void Add(User entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new System.NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }


        public System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
