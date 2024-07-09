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
    public class BranchDTO
    {


        [Required(ErrorMessage = "Name is Required")]
        [MinLength(3, ErrorMessage = "lenght must be at leat 3")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[\d]{3,}", ErrorMessage ="name must start with at least 3 charachters")]
        public string Name { get; set; }


        [Required(ErrorMessage ="this field is required")]
        public int GovernmentId { get; set; }
    }
}
