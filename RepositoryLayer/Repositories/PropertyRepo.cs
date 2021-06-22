using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface IPropertyRepo : IRepository<tblProperty>
    {
        // Define Methods
    }

    public class PropertyRepo : Repository<tblProperty>, IPropertyRepo
    {
        public PropertyRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}