﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class LoginDTO
    {
        public string? email { get; set; }
        public string? userName { get; set; }
        public string password { get; set; }
        public bool RemembreMe {  get; set; }

    }
}
