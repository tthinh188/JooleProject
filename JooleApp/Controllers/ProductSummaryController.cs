using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using DomainLayer;
using JooleApp.ViewModel;

namespace JooleApp.Controllers
{
    public class ProductSummaryController : Controller
    {
        public static int subcategory;
        public static int category;

        // GET: ProductSummary
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductSummary(ProductFilter filterOptions)
        {
            Service service = new Service();


            if (filterOptions.category == 0)
                filterOptions.category = 1;
            if (filterOptions.subcategory == 0)
                filterOptions.subcategory = 1;
            category = filterOptions.category;
            subcategory = filterOptions.subcategory;

            ViewBag.CategoryChosen = service.getCategoryByID(filterOptions.category);
            ViewBag.SubCategoryChosen = service.getSubCategoryByID(filterOptions.subcategory);


            filterOptions.typeFilter = service.getAllTypesBySubCategoryID(filterOptions.subcategory);

            filterOptions.techSpecs = new List<TechnicalSpecification>();
            List<tblTechSpecFilter> techSpec = service.getTechSpecsBySubCategoryID(filterOptions.subcategory).ToList();
            foreach (var specification in techSpec)
            {
                TechnicalSpecification tempSpec = new TechnicalSpecification();
                tempSpec.propertyID = specification.PropertyID;
                tempSpec.min = specification.MinValue;
                tempSpec.max = specification.MaxValue;

                filterOptions.techSpecs.Add(tempSpec);

            }

            foreach (TechnicalSpecification specification in filterOptions.techSpecs)
            {
                specification.name = service.getPropertyNameByPropertyID(specification.propertyID).Trim();
            }


            filterOptions.products = service.getProductDescriptionsBySubCategoryID(filterOptions.subcategory).ToList();

            return View(filterOptions);
        }

        public ActionResult ClearFilters(ProductFilter filterOptions)
        {
            Service service = new Service();
            ProductFilter cleared = new ProductFilter();

            cleared.subcategory = subcategory;
            cleared.category = category;
            ViewBag.CategoryChosen = service.getCategoryByID(cleared.category);
            ViewBag.SubCategoryChosen = service.getSubCategoryByID(cleared.subcategory);


            cleared.typeFilter = new List<string>();
            cleared.typeFilter = service.getAllTypesBySubCategoryID(cleared.subcategory);
            cleared.products = service.getProductDescriptionsBySubCategoryID(cleared.subcategory).ToList();

            cleared.techSpecs = new List<TechnicalSpecification>();
            List<tblTechSpecFilter> techSpec = service.getTechSpecsBySubCategoryID(cleared.subcategory).ToList();
            foreach (var specification in techSpec)
            {
                TechnicalSpecification tempSpec = new TechnicalSpecification();
                tempSpec.propertyID = specification.PropertyID;
                tempSpec.min = specification.MinValue;
                tempSpec.max = specification.MaxValue;

                cleared.techSpecs.Add(tempSpec);

            }

            foreach (TechnicalSpecification specification in cleared.techSpecs)
            {
                specification.name = service.getPropertyNameByPropertyID(specification.propertyID);
            }


            return View("ProductSummary", cleared);
        }


        [HttpPost]
        public ActionResult GetSearchingData(ProductFilter filterOptions, FormCollection fobj)
        {
            Service service = new Service();

            // Create a dropdown list
            filterOptions.subcategory = subcategory;
            filterOptions.category = category;
            ViewBag.CategoryChosen = service.getCategoryByID(filterOptions.category);
            ViewBag.SubCategoryChosen = service.getSubCategoryByID(filterOptions.subcategory);

            filterOptions.techSpecs = new List<TechnicalSpecification>();
            List<tblTechSpecFilter> techSpec = service.getTechSpecsBySubCategoryID(filterOptions.subcategory).ToList();
            foreach (var specification in techSpec)
            {
                TechnicalSpecification tempSpec = new TechnicalSpecification();
                tempSpec.propertyID = specification.PropertyID;
                tempSpec.min = specification.MinValue;
                tempSpec.max = specification.MaxValue;

                filterOptions.techSpecs.Add(tempSpec);

            }

            foreach (TechnicalSpecification specification in filterOptions.techSpecs)
            {
                specification.name = service.getPropertyNameByPropertyID(specification.propertyID).Trim();
            }


            filterOptions.typeFilter = service.getAllTypesBySubCategoryID(filterOptions.subcategory);

            // Get Product Descriptions
            IEnumerable<tblProduct> productlist = service.getProductDescriptionsBySubCategoryID(filterOptions.subcategory);
            filterOptions.products = productlist.ToList();

            // For Model Year filter
            if (filterOptions.modelYear1 != null)
                filterOptions.products = filterOptions.products.Where(x => x.ModelYear >= Convert.ToInt32(filterOptions.modelYear1)).ToList();
            if (filterOptions.modelYear2 != null)
                filterOptions.products = filterOptions.products.Where(x => x.ModelYear <= Convert.ToInt32(filterOptions.modelYear2)).ToList();


            // For Product Type filter
            //filterOptions.productType = fobj["hidden1"].ToString();  // use formcollection
            if (filterOptions.productType != null)
            {
                List<tblProduct> productsFiltered = new List<tblProduct>();
                foreach (var product in filterOptions.products)
                {
                    foreach (var property in product.tblPropertyValues)
                    {
                        if (property.Value.Trim() == filterOptions.productType.Split(':')[1].Trim())
                            productsFiltered.Add(product);
                    }
                }
                filterOptions.products = productsFiltered;
            }

            // For Tech Spec filter
            List<tblProduct> productsFiltered_Tech = new List<tblProduct>();

            bool selected = true;
            foreach (var product in filterOptions.products)
            {
                selected = true;
                foreach (var techSpecs in filterOptions.techSpecs)
                {
                    if (service.getPropertyValueByPropertyAndProductID(techSpecs.propertyID, product.ProductID) != -1)
                    {
                        if (Convert.ToDouble(service.getPropertyValueByPropertyAndProductID(techSpecs.propertyID, product.ProductID)) < Convert.ToDouble(fobj[techSpecs.propertyID + "min"]) ||
                            Convert.ToDouble(service.getPropertyValueByPropertyAndProductID(techSpecs.propertyID, product.ProductID)) > Convert.ToDouble(fobj[techSpecs.propertyID + "max"]))
                        {
                            selected = false;                            
                        }
                    }
                    else
                    {
                        selected = false;
                    }

                }
                if (selected) productsFiltered_Tech.Add(product);

            }
            filterOptions.products = productsFiltered_Tech;




            return View("ProductSummary", filterOptions);
        }


        public ActionResult SeeDetails(int productID)
        {
            Service service = new Service();
            ViewBag.productDetails = service.getProductDetailsByID(productID);

            return View();
        }

    }
}