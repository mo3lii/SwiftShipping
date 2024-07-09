using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class DeliveryManDTO
    {

        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "lenght must be at leat 3")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[\d]{3,}", ErrorMessage = "name must start with at least 3 charachters")]
        public string name { get; set; }

        [Required(ErrorMessage = "address is Required")]
        [MinLength(3, ErrorMessage = "lenght must be at leat 3")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[\d]{3,}", ErrorMessage = "address must start with at least 3 charachters")]
        public string address { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"[\w]{4,}[a-zA-Z0-9]{0,}\@(gmail|yahoo|hotmail)\.com$", ErrorMessage = "Enter a valid email")]
        public string email { get; set; }

        [Required(ErrorMessage = "username is Required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "password is Required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\dA-Za-z]{4,})(?=.*[_$@|/\\.&])[A-Za-z\d_$@|/\\.&]{8,}$", ErrorMessage = "ErrorMessage = \"Password must contain at least 1 uppercase letter, 4 alphanumeric characters, and at least one special character.\"")]
        public string password { get; set; }

        [RegularExpression(@"^[0](10|11|12|15)[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "branch is Required")]
        public int branchId { get; set; }

    }
}
