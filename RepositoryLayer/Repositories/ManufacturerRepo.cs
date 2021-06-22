using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface IManufacturerRepo : IRepository<tblManufacturer>
    {
        // Define Methods
    }

    public class ManufacturerRepo : Repository<tblManufacturer>, IManufacturerRepo
    {
        public ManufacturerRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}