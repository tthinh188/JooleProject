using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface IUserRepo: IRepository<tblUser>
    {
        // Define Methods
    }

    public class UserRepo : Repository<tblUser>, IUserRepo
    {
        public UserRepo(DbContext context): base(context)
        {

        }
        // Implement Methods
    }
}
