﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI.Models
{
    public class UserCredModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
        public string type { get; set; }
    }
    public class AutorizeModel
    {
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string lastname { get; set; }
    }
}
