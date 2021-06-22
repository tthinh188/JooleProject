using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainLayer;
using ServiceLayer; 


namespace JooleApp.ViewModel
{
    public class ProductViewModel
    {
        // product
        public tblProduct Product { get; set; }

        // list of products
        public List<tblProduct> Products { get; set; }

    }

}