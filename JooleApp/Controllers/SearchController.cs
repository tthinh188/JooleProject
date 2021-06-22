using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceLayer;
using DomainLayer;
using System.Web.Security;
using JooleApp.ViewModel;
namespace JooleApp.Controllers
{
    public class SearchController : Controller
    {
        
        [HttpPost]
        public ActionResult SearchResult(FormCollection form)
        {
            Service service = new Service();
            ProductFilter filter = new ProductFilter();

            if (form.Get("subCategoryDropdown") == "" ||
                form.Get("mainCategoryDropdown") == "") { 
                
                return RedirectToAction("SearchPage");
            
            }
            int j = int.Parse(form.Get("mainCategoryDropdown"));
            System.Diagnostics.Debug.WriteLine("MAIN CATEGORY:-" + form.Get("mainCategoryDropdown") + "-");
            System.Diagnostics.Debug.WriteLine("SUB CATEGORY:-" + form.Get("subCategoryDropdown") + "-");
            //System.Diagnostics.Debug.WriteLine("SUB CATEGORY ID:-" + service.getSubCategoryIDbyName(j, form.Get("subCategoryDropdown").Trim()) + "-");
            //int i = int.Parse(form.Get("subCategoryDropdown"));
            
            int i = service.getSubCategoryIDbyName(j, form.Get("subCategoryDropdown").Trim());

            if (i == -1) i = 0;
            if (j == -1) j = 0;


            filter.subcategory = i;
            filter.category = j;

            //System.Diagnostics.Debug.WriteLine("MAIN CATEGORY:-" + j);
            //System.Diagnostics.Debug.WriteLine("SUB CATEGORY:-" + i);

            return RedirectToAction("ProductSummary","ProductSummary", filter);

        }


        [HttpGet]
        public ActionResult SearchPage()
        {
            Service service = new Service();
            SearchViewModel searchModel = new SearchViewModel();
            Dictionary<tblCategory, List<tblSubCategory>> subcategorySet = new Dictionary<tblCategory, List<tblSubCategory>>();
            List<tblCategory> mainCategorySet = new List<tblCategory>();
            IEnumerable<tblCategory> list = service.getAllCategories();

            foreach (tblCategory i in list) {

                List<tblSubCategory> subList = service.getSubCategorybyCategoryID(i.CategoryID);
                subcategorySet.Add(i,subList);
                mainCategorySet.Add(i);
            }
            searchModel.subcategories = subcategorySet;
            searchModel.maincategories = mainCategorySet;

            this.Session["layoutSearchModel"] = searchModel;

            return View(searchModel);
        }

        public ActionResult RenderSearchBar()
        {

            Service service = new Service();
            SearchViewModel searchModel = new SearchViewModel();
            Dictionary<tblCategory, List<tblSubCategory>> subcategorySet = new Dictionary<tblCategory, List<tblSubCategory>>();
            List<tblCategory> mainCategorySet = new List<tblCategory>();
            IEnumerable<tblCategory> list = service.getAllCategories();

            foreach (tblCategory i in list)
            {

                List<tblSubCategory> subList = service.getSubCategorybyCategoryID(i.CategoryID);
                subcategorySet.Add(i, subList);
                mainCategorySet.Add(i);
            }
            searchModel.subcategories = subcategorySet;
            searchModel.maincategories = mainCategorySet;

            //var currentUser = DataManager.GetCurrentUser();
            return PartialView("_SearchBar", searchModel);
        }

    }
}