using Microsoft.AspNetCore.Identity;
using SwiftShipping.DataAccessLayer.Models;
using SwiftShipping.DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.Services
{
    public class GovernmentService
    {
        private UnitOfWork unit;
        public GovernmentService(UnitOfWork _unit)
        {
            unit = _unit;
        }

        public bool AddGovernment(string name)
        {
            try { 
            unit.GovernmentRipository.Insert(new Government() { Name = name });
            unit.SaveChanges();
            }catch { 
            return false;
            }
            return true;
        }
    }
}
