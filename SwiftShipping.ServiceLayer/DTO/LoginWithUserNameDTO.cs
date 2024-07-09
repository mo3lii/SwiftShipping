﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class LoginWithUserNameDTO
    {

        [Required(ErrorMessage = "username field is Required")]
        public string userName { get; set; }


        [Required(ErrorMessage = "password field is Required")]
        public string password { get; set; }
        public bool RemembreMe { get; set; }
    }
}
