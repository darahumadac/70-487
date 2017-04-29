using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.EntityFrameworkRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    
    public class BaseUserRepositoryTest
    {
        protected readonly IRepository<User> _userRepository;

        protected BaseUserRepositoryTest()
        {
            
        }

        private void RemoveUser(string username)
        {
            AppEntities db = new AppEntities();
            User user = db.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        private void UndoUserFirstName()
        {
            AppEntities db = new AppEntities();
            User updateUser = db.Users.FirstOrDefault(u => u.Username.Equals("darahumadac"));
            updateUser.FirstName = "Darah";
            db.SaveChanges();
        }

        protected BaseUserRepositoryTest(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [TestMethod]
        public virtual void GetAllUsers()
        {
            IEnumerable<User> users = _userRepository.GetAll();
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public virtual void GetUserById()
        {
            User user = _userRepository.GetById(1);

            Assert.IsNotNull(user);
            Assert.AreEqual("Darah", user.FirstName);
        }

        [TestMethod]
        public virtual void AddUser()
        {
            User newUser = new User()
            {
                FirstName = "Jane",
                LastName = "Doe",
                Username = "janedoe01",
                City = "Makati"
            };

            _userRepository.Add(newUser);

            User retrievedUser = _userRepository.GetAll().FirstOrDefault(u => u.Username.Equals("janedoe01"));
            Assert.IsNotNull(retrievedUser);

            RemoveUser("janedoe01");
        }

        [TestMethod]
        public void UpdateUser()
        {
            AppEntities db = new AppEntities();

            User updatedUser = UpdateUserFirstName(db, "Updated Darah");

            Assert.IsNotNull(db.Users.FirstOrDefault(u => u.FirstName.Equals("Updated Darah")));

            UndoUserFirstName();
        }

        private User UpdateUserFirstName(AppEntities db, string newFirstName)
        {
            
            User updateUser = db.Users.FirstOrDefault(u => u.Username.Equals("darahumadac"));
            if (updateUser != null)
            {
                updateUser.FirstName = newFirstName;

                try
                {
                    _userRepository.Update(updateUser);
                }
                catch (NotImplementedException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    //don't do anything
                }
                finally
                {
                    updateUser = new AppEntities().Users.FirstOrDefault(u => u.Username.Equals("darahumadac"));
                }
            }

            return updateUser;

        }

        [TestMethod]
        public void Failed_UpdateUser_Must_Rollback()
        {
            AppEntities db = new AppEntities();

            User updateUser = UpdateUserFirstName(db, null);

            Assert.AreEqual("Darah", updateUser.FirstName);

        }

        [TestMethod]
        public virtual void GetAllUsersAsync()
        {
            var users = _userRepository.GetAllAsync();
            Assert.IsTrue(users.Result.Any());

        }




    }
}
