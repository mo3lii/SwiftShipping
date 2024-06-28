﻿using SwiftShipping.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class BranchDTO
    {
        public string Name { get; set; }
        public int GovernmentId { get; set; }
    }
}
