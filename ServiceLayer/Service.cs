using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using DomainLayer;
using System.Text.RegularExpressions;

namespace ServiceLayer

{

    public class Service
    {

        public static readonly JooleDatabaseEntities context = new JooleDatabaseEntities();
        UnitOfWork unw = new UnitOfWork(context);
        public Service()
        {

        }


        // Validate User
        public Tuple<bool, string> LoginUser(string email, string password)
        {
            var result = unw.user.GetAll();
            bool login = false;
            string image = "";

            if (Regex.IsMatch(email, "@"))  // if the input is an email address
            {
                foreach (var item in result)
                {
                    if (item.UserEmail.Trim() == email && item.UserPassword.Trim() == password)
                    {
                        login = true;
                        if (item.UserImage != null)
                            image = item.UserImage.Trim();
                        else
                            image = null;
                    }
                }
            }
            else  // if the input is username
            {
                foreach (var item in result)
                {
                    if (item.UserName.Trim() == email && item.UserPassword.Trim() == password)
                    {
                        login = true;
                        if (item.UserImage != null)
                            image = item.UserImage.Trim();
                        else
                            image = null;
                    }
                }
            }

            return new Tuple<bool, string>(login, image);
        }


        // Add User
        public void insertUser(tblUser user)
        {
            unw.user.Insert(user);
            unw.SaveChanges();
        }


        // Check if Username and Email are unique
        public bool checkLoginID(string username, string email)
        {
            var result = unw.user.GetAll();
            foreach (var item in result)
            {
                if (username == item.UserName.Trim() || email == item.UserEmail.Trim())
                    return false;
            }
            return true;
        }



        //Return all CategoryName in tblCategory as List<String>
        public IEnumerable<tblCategory> getAllCategories()
        {

            //List<tblCategory> categories = new List<tblCategory>();
            var result = unw.category.GetAll();
            return result;
        }


        public int getSubCategoryIDbyName(int categoryID, string subCatName) {
            var res = unw.subCategory.GetAll();

            foreach (var item in res) {

                if (item.CategoryID == categoryID && item.CategoryName.Trim()==subCatName.Trim()) {
                    return item.SubCategoryID;
                }
            }

            return -1;
        }

        //get all SubCategory by CategoryID
        public List<tblSubCategory> getSubCategorybyCategoryID(int ID)
        {
            //List<tblCategory> categories = new List<tblCategory>();
            var result = unw.subCategory.GetAll();
            var returnSet = new List<tblSubCategory>();
            foreach (tblSubCategory item in result)
            {
                if (item.CategoryID == ID) returnSet.Add(item);
            }


            return returnSet;
        }


        // Get all types by SubCategoryID
        public List<string> getAllTypesBySubCategoryID(int subCategoryID)
        {
            var result = unw.typeFilter.GetAll();

            List<String> types = new List<String>();
            foreach (var type in result)
            {
                if (type.SubCategoryID.ToString() == subCategoryID.ToString())
                {
                    types.Add(type.TypeName + ": " + type.TypeOptions);
                }
            }

            return types;
        }


        // Get product descriptions by SubCategoryID, later use ViewBag to pass data from Controller to View
        public List<Dictionary<string, string>> getProductDescriptionsBySubCategoryID_ViewBag(int subCategoryID)
        {
            var result = unw.product.GetAll();
            List<Dictionary<string, string>> productDescriptions = new List<Dictionary<string, string>>();
            foreach (var item in result)
            {
                if (item.SubCategoryID == subCategoryID)
                {
                    Dictionary<string, string> productDescription = new Dictionary<string, string>();
                    productDescription.Add("ProductID", item.ProductID.ToString());
                    productDescription.Add("Image", item.ProductImage.Trim());
                    productDescription.Add("Manufacture", getManufactureByID(item.ManufacturerID));
                    productDescription.Add("Product Name", item.ProductName);
                    productDescription.Add("Series", item.Series);
                    productDescription.Add("Model", item.Model);
                    productDescription.Add("Model Year", item.ModelYear.ToString());

                    productDescriptions.Add(productDescription);
                }
            }
            return productDescriptions;
        }


        // Get product descriptions by SubCategoryID
        public IEnumerable<tblProduct> getProductDescriptionsBySubCategoryID(int subCategoryID)
        {
            var result = unw.product.GetAll();
            result = result.Where(x => x.SubCategoryID == subCategoryID);

            return result;
        }



        public string getManufactureByID(int manufactureID)
        {
            return unw.manufacturer.GetByID(manufactureID).ManufacturerName;
        }

        public Dictionary<string, Dictionary<string, string>> getProductDetailsByID(int productID)
        {
            Dictionary<string, Dictionary<string, string>> details = new Dictionary<string, Dictionary<string, string>>();
            var product = unw.product.GetByID(productID);
            Dictionary<string, string> productDescription = new Dictionary<string, string>();

            productDescription.Add("Image", product.ProductImage);
            productDescription.Add("Manufacture", getManufactureByID(product.ManufacturerID));
            productDescription.Add("Product Name", product.ProductName);
            productDescription.Add("Series", product.Series);
            productDescription.Add("Model", product.Model);
            productDescription.Add("Model Year", product.ModelYear.ToString());
            details.Add("PRODUCT_DESCRIPTION", productDescription);

            Dictionary<string, string> productType = new Dictionary<string, string>();
            Dictionary<string, string> productTechSpec = new Dictionary<string, string>();

            var propertyValues = unw.propertyValue.GetAll();

            foreach (var property in propertyValues)
            {
                if (property.ProductID == productID)
                {
                    if (property.tblProperty.IsType)
                    {
                        productType.Add(property.tblProperty.PropertyName, property.Value);
                    }
                    else
                    {
                        productTechSpec.Add(property.tblProperty.PropertyName, property.Value);
                    }
                }
            }
            details.Add("TYPE", productType);
            details.Add("TECHNICAL SPECIFICATIONS", productTechSpec);
            return details;
        }

        public IEnumerable<tblTechSpecFilter> getTechSpecsBySubCategoryID(int subCategoryID)
        {
            var result = unw.techFilter.GetAll();
            result = result.Where(x => x.SubCategoryID == subCategoryID);

            return result;
        }

        public string getPropertyNameByPropertyID(int propertyID)
        {
            return unw.property.GetByID(propertyID).PropertyName;
        }


        public string getSubCategoryByID(int ID)
        {
            return unw.subCategory.GetByID(ID).CategoryName;
        }
        public string getCategoryByID(int ID)
        {
            return unw.category.GetByID(ID).CategoryName;
        }


        public int getPropertyValueByPropertyAndProductID(int propertyID, int productID)
        {
            
            List<tblPropertyValue> propertyValues = unw.propertyValue.GetAll().ToList();
            propertyValues = propertyValues.Where(x => x.ProductID == productID && x.PropertyID == propertyID).ToList();

            if (propertyValues.Count == 1)
            {
                int val = Int32.Parse(propertyValues[0].Value);
                return val;
            }
            else
            {
                return -1;
            }
        }
    }

}
