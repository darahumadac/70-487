using System;
using DataAccess.AdoNetSqlRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AdoNetSqlRepository : BaseUserRepositoryTest
    {
        public AdoNetSqlRepository() : base(new AdoNetSqlUserRepository())
        {   
        }

    }
}
