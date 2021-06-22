using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DomainLayer;

namespace JooleApp.ViewModel
{
    public class ProductFilter
    {
        public int category { get; set; }

        public int subcategory { get; set; }

        public List<tblProduct> products { get; set; }

        public string modelYear1 { get; set; }

        public string modelYear2 { get; set; }

        public List<string> typeFilter { get; set; }

        public string productType { get; set; }

        public List<TechnicalSpecification> techSpecs { get; set; }
    }
}