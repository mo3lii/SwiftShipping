using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SwiftShipping.API
{
    public static class JwtTokenHelper
    {
        public static string GenerateToken(List<Claim> _userdata)
        {
            List<Claim> userdata = _userdata;

            string key = "welcome to my secret key Ahmed Mohamed Samir";
            var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var signingcer = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: userdata,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingcer
                );

            //token object => encoded string
            var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenstring;
        }
    }
}
