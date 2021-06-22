using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DomainLayer;

namespace RepositoryLayer.Repositories
{
    public interface ITechSpecFilterRepo : IRepository<tblTechSpecFilter>
    {
        // Define Methods
    }

    public class TechSpecFilterRepo : Repository<tblTechSpecFilter>, ITechSpecFilterRepo
    {
        public TechSpecFilterRepo(DbContext context) : base(context)
        {

        }
        // Implement Methods
    }
}