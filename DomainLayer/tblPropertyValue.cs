//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DomainLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPropertyValue
    {
        public int PropertyID { get; set; }
        public int ProductID { get; set; }
        public string Value { get; set; }
    
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblProperty tblProperty { get; set; }
    }
}
