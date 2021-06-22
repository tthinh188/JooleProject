using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DomainLayer
{
    public class UserMetadata
    {
        [Required(ErrorMessage = "Please Enter The User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter The Login ID")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please Enter The Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword", ErrorMessage = "The password and the confirm password do not match.")]
        public string UserConfirmPassword { get; set; }
    }
}
