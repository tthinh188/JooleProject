using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface IPropertyValueRepo : IRepository<tblPropertyValue>
    {
        // Define Methods
    }

    public class PropertyValueRepo : Repository<tblPropertyValue>, IPropertyValueRepo
    {
        public PropertyValueRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}