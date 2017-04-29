using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.UserService.Contracts;


namespace WCF.UserService
{
    /// <summary>
    /// TOPIC:  WCF - Configuring services programmatically and using proxy classes
    /// 
    /// UserService is for WCF project using Class Library template.
    /// This service will be used to retrieve list of users
    /// </summary>
    
    public class UserService : IUserService
    {

        public IEnumerable<AppUser> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetUsers(string city)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void AddUser(AppUser user)
        {
            throw new NotImplementedException();
        }
    }
}
