using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Class
{
    public class TokenClass
    {
        public string Token { get; set; }
        public DateTime Expirydate { get; set; }
    }
    public class JWTToken
    {
        public long exp { get; set; }
    }
}
