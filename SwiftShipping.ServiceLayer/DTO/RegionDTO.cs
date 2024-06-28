﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class RegionDTO
    {
        public string Name {  get; set; }
        public decimal NormalPrice { get; set; }
        public decimal PickupPrice { get; set; }
        public int GovernmentId { get; set; }

    }
}
