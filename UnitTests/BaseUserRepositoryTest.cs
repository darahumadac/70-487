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
        private readonly IRepository<User> _userRepository;

        protected BaseUserRepositoryTest()
        {
            
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
            AppEntities db = new AppEntities();
            User janeDoe = db.Users.FirstOrDefault(u => u.Username.Equals("janedoe01"));
            if (janeDoe != null)
            {
                db.Users.Remove(janeDoe);
                db.SaveChanges();
            }

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
        }

        [TestMethod]
        public virtual void GetAllUsersAsync()
        {
            var users = _userRepository.GetAllAsync();
            Assert.IsTrue(users.Result.Any());

        }

    }
}
