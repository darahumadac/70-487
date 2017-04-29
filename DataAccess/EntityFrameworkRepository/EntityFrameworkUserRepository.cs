using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFrameworkRepository
{
    public class EntityFrameworkUserRepository : IRepository<User>
    {
        public IEnumerable<User> GetAll()
        {
            using (var db = new AppEntities())
            {
                //Don't forget ToList or this will throw InvalidOperationException for disposing DbContext
                return db.Users.ToList(); 
            }
        }

        public User GetById(int id)
        {
            //Using DbContext
            //using (var db = new AppEntities())
            //{
            //    return db.Users.Find(id);
            //}

            //Using ObjectContext
            //Notes: the FROM syntax:   SELECT VALUE <Alias> FROM <ConnectionStringName>.<TableName> AS <Alias>
             using (var dbObjectContext = new AppEntitiesObjContext())
             {
                 string queryString = @"SELECT VALUE u FROM AppEntities.Users AS u WHERE u.Id = @id";
                 ObjectQuery<User> result =  
                     dbObjectContext.CreateQuery<User>(queryString, new ObjectParameter("id", id));

                 return result.FirstOrDefault();
             }
            
        }

        public User GetByIdUsingDbContext(int id)
        {
            using (var db = new AppEntities())
            {
                return db.Users.Find(id);
            }

        }

        public void Add(User entity)
        {
            using (var db = new AppEntities())
            {
                db.Users.Add(entity);
                db.SaveChanges();
            }
        }


        public void Update(User entity)
        {
            using (var db = new AppEntities())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            using (var db = new AppEntities())
            {
                User userToDelete = db.Users.Find(id);
                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    db.SaveChanges();
                }
            }
        }


        public async Task<IEnumerable<User>> GetAllAsync()
        {
            List<User> users = new List<User>();
            //Using EntityConnection, EntityCommand, and Async methods
            using (var db = new EntityConnection("name=AppEntities"))
            {
                await db.OpenAsync();

                EntityCommand command = db.CreateCommand();
                command.CommandText = "SELECT VALUE u FROM AppEntities.USERS AS u";

                using (var entityDataReader = 
                    await command.ExecuteReaderAsync(CommandBehavior.SequentialAccess))
                {

                    while (entityDataReader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = (int)entityDataReader.GetValue(0),
                            FirstName = (string)entityDataReader.GetValue(1),
                            LastName = (string)entityDataReader.GetValue(2),
                            City = (string)entityDataReader.GetValue(3),
                            Username = (string)entityDataReader.GetValue(4)

                        });
                    }
                }
                
            }

            return users;
        }
    }
}
