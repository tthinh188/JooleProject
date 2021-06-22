using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface IProductRepo : IRepository<tblProduct>
    {
        // Define Methods
    }

    public class ProductRepo : Repository<tblProduct>, IProductRepo
    {
        public ProductRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}