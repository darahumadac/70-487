using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.EntityFrameworkRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class EntityFrameworkTests : BaseUserRepositoryTest
    {

        public EntityFrameworkTests() : base(new EntityFrameworkUserRepository())
        {

        }

        [TestMethod]
        public void GetUserByIdDbContext()
        {
            EntityFrameworkUserRepository repo = new EntityFrameworkUserRepository();
            User user = repo.GetByIdUsingDbContext(1);

            Assert.IsNotNull(user);
            Assert.AreEqual("Darah", user.FirstName);
        }

    }
}
