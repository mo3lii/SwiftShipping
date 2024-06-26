﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class OrderDTO
    {
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string address { get; set; }
        public int governmentId { get; set; }
        public int regionId { get; set; }
        public bool isShippedToVillage { get; set; }
        public string? villageName { get; set; }
        public float weight { get; set; }
        public string note { get; set; }
    }
}
