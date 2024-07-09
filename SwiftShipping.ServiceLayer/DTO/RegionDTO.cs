using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class RegionDTO
    {

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Maximum length is 50")]
        [RegularExpression(@"^[a-zA-Z\s]{3,}", ErrorMessage = "name must start with at least 3 charachters")]
        public string Name {  get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid normal price")]
        public decimal NormalPrice { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid pickup price")]
        public decimal PickupPrice { get; set; }

        [RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid governmentId")]
        public int GovernmentId { get; set; }

    }
}
