using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface ISubCategoryRepo : IRepository<tblSubCategory>
    {
        // Define Methods
    }

    public class SubCategoryRepo : Repository<tblSubCategory>, ISubCategoryRepo
    {
        public SubCategoryRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}