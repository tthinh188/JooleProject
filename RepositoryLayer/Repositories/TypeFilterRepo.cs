using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface ITypeFilterRepo : IRepository<tblTypeFilter>
    {
        // Define Methods
    }

    public class TypeFilterRepo : Repository<tblTypeFilter>, ITypeFilterRepo
    {
        public TypeFilterRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}