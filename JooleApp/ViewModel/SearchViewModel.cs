using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainLayer;


namespace JooleApp.ViewModel
{
    [Serializable]
    public class SearchViewModel
    {
        public Dictionary<tblCategory, List<tblSubCategory>> subcategories { get; set; }
        public List<tblCategory> maincategories { get; set; }

    }
}