using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class LoginWithEmailDTO
    {
        [Required(ErrorMessage = "email field is Required")]
        public string email { get; set; }


        [Required(ErrorMessage = "password field is Required")]
        public string password { get; set; }


        public bool RemembreMe { get; set; }
    }
}
