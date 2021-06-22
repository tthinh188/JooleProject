using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface ICategoryRepo : IRepository<tblCategory>
    {
        // Define Methods
    }

    public class CategoryRepo : Repository<tblCategory>, ICategoryRepo
    {
        public CategoryRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}