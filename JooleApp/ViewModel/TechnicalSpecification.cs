using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JooleApp.ViewModel
{
    public class TechnicalSpecification
    {
        public string name { get; set; }

        public int propertyID { get; set; }
        public int min { get; set; }

        public int max { get; set; }
    }
}