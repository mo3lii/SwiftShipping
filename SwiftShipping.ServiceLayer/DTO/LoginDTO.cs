using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class LoginDTO
    {

        public string? email { get; set; }
        public string? userName { get; set; }

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\dA-Za-z]{4,})(?=.*[_$@|/\\.&])[A-Za-z\d_$@|/\\.&]{8,}$", ErrorMessage = "Password must contain at least 1 uppercase letter, 4 alphanumeric characters, and at least one special character.")]
        public string password { get; set; }
        public bool RemembreMe {  get; set; }

    }
}
