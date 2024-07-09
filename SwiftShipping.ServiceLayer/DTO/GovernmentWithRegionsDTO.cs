using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class GovernmentWithRegionsDTO
    {

        [Required(ErrorMessage = "Id field is Required")]

        public int Id { get; set; }

        [Required(ErrorMessage = "Name field is Required")]
        public string Name { get; set; }
        public List<RegionGetDTO> Regions { get; set; }
    }
}
