using System.ComponentModel.DataAnnotations;

namespace SwiftShipping.ServiceLayer.DTO
{
    public class SellerDTO
    {
        //[Required(ErrorMessage = "Name is Required")]
        //[MaxLength(50, ErrorMessage = "maximum length is 50")]
        //[RegularExpression(@"^[a-zA-Z\s]{3}.*", ErrorMessage = "name must start with at least 3 charachters")]

        public string name { get; set; }


        //[Required(ErrorMessage = "address is Required")]
        //[MaxLength(50, ErrorMessage = "maximum length is 50")]
        //[RegularExpression(@"^[a-zA-Z\s]{3}.*$", ErrorMessage = "address must start with at least 3 characters")]
        public string address { get; set; }


        //[Required(ErrorMessage = "Email is Required")]
        //[RegularExpression(@"[\w]{4,}[a-zA-Z0-9]{0,}\@(gmail|yahoo|hotmail)\.com$", ErrorMessage = "Enter a valid email")]
        public string email { get; set; }

        //[Required(ErrorMessage = "username is Required")]
        public string userName { get; set; }

        //[Required(ErrorMessage = "password is Required")]
        //[RegularExpression(@"^(?=.*[A-Z])(?=.*[\dA-Za-z]{4,})(?=.*[_$@|/\\.&])[A-Za-z\d_$@|/\\.&]{8,}$", ErrorMessage = "Password must contain at least 1 uppercase letter, 4 alphanumeric characters, and at least one special character.")]
        public string password { get; set; }

        //[RegularExpression(@"^[0](10|11|12|15)[0-9]{8}$", ErrorMessage = "Invalid phone number format")]
        public string phoneNumber { get; set; }

        //[RegularExpression(@"^[a-zA-Z\s]{3}.*", ErrorMessage = "store name must start with at least 3 charachters")]
        public string storeName { get; set; }

        //[RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid regionId")]
        public int regionId { get; set; }

        //[RegularExpression(@"^[1-9]{1}[0-9]*", ErrorMessage = "enter valid branchId")]
        public int branchId { get; set; }
    }
}
