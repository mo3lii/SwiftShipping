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
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[a-zA-Z\s]{3}.*", ErrorMessage = "name must start with at least 3 charachters")]
        public string Name { get; set; }


        [Required(ErrorMessage ="this field is required")]
        [RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid governmentId")]
        public int GovernmentId { get; set; }
    }
}
