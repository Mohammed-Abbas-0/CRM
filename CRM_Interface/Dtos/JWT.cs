﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Interface.Dtos
{
    public class JWT
    {
        public required string Key {  get; set; }
        public required string Audience {  get; set; }
        public required string Issuer {  get; set; }
        public required double Expiration {  get; set; }

    }
}
