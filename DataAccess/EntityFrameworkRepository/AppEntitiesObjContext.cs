using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntityFrameworkRepository
{
    public class AppEntitiesObjContext : ObjectContext
    {
        public AppEntitiesObjContext() : base("name=AppEntities")
        {
            
        }
        
    }
}
