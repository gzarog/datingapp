﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace datingapp.api.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }





    }
}
