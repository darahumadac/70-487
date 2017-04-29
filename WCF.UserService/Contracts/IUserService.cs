using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF.UserService.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract(Name = "GetAllUsers")]
        IEnumerable<AppUser> GetUsers();

        [OperationContract(Name = "GetUsersByCity")]
        IEnumerable<AppUser> GetUsers(string city);

        [OperationContract]
        AppUser GetUser(int id);

        [OperationContract]
        void AddUser(AppUser user);
    }
}
