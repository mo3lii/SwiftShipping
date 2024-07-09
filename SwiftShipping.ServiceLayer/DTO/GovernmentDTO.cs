using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class GovernmentDTO
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "maximum length is 50")]
        [RegularExpression(@"^[a-zA-Z\s]{3}.*", ErrorMessage = "name must start with at least 3 charachters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "IsActive field is Required")]
        public bool IsActive { get; set; }


        [Required(ErrorMessage = "IsDeleted ield is Required")]
        public bool IsDeleted { get; set; }

    }
}
