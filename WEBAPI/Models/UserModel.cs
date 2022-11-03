using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class UserModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
       // public DateTime birthdate { get; set; }
        public string ConNo { get; set; }
        public int position { get; set; }
        public int status { get; set; }
        public string email { get; set; }
        //public string _password { get; set; }
    }

}
