using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;


namespace Connect2GetherWPF.Models
{
    internal class JWTtoken
    {
       
            string Name { get; set; }
            string role { get; set; }
            int id { get; set; }

        
        public static JwtSecurityToken JWTokenDecoder(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            return jwtHandler.ReadJwtToken(token);
        }
        public static int JWTokenDecodeID(string jwt)
        {
            JwtSecurityToken token = JWTokenDecoder(jwt);
            return int.Parse(token.Claims.First(claim => claim.Type == "id").Value);
        }

        public static string JWTokenDecodeName(string jwt)
        {
            JwtSecurityToken token = JWTokenDecoder(jwt);
            return token.Claims.First(claim => claim.Type == "Name").Value;
        }

        public static string JWTokenDecodeRole(string jwt)
        {
            JwtSecurityToken token = JWTokenDecoder(jwt);
            return token.Claims.First(claim => claim.Type == "role").Value;
        }






        public JWTtoken(string jwt)
        {

            
            this.id = JWTokenDecodeID(jwt);
            this.role = JWTokenDecodeRole(jwt);
            this.Name = JWTokenDecodeName(jwt);

        }
    }
}