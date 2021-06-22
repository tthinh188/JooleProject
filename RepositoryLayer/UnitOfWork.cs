using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Repositories;

namespace RepositoryLayer
{
    public class UnitOfWork: IDisposable
    {
        DbContext Context;
        public IUserRepo user;
        public ICategoryRepo category;
        public IManufacturerRepo manufacturer;
        public IProductRepo product;
        public IPropertyRepo property;
        public IPropertyValueRepo propertyValue;
        public ISubCategoryRepo subCategory;
        public ITechSpecFilterRepo techFilter;
        public ITypeFilterRepo typeFilter;


        // Constructor that initializes all the repositories
        public UnitOfWork(DbContext context)
        {
            this.Context = context;
            user = new UserRepo(Context);
            category = new CategoryRepo(Context);
            manufacturer = new ManufacturerRepo(Context);
            product = new ProductRepo(Context);
            property = new PropertyRepo(Context);
            propertyValue = new PropertyValueRepo(Context);
            subCategory = new SubCategoryRepo(Context);
            techFilter = new TechSpecFilterRepo(Context);
            typeFilter = new TypeFilterRepo(Context);


        }



        // Save any changes to the database
        public void SaveChanges()
        {
            Context.SaveChanges();
        }



        // Disposes the unit of work after it has been used
        public void Dispose()
        {
            Context.Dispose();
        }


    }


}
