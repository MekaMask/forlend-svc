﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forlend.Models
{
    public class CreateItemRequest
    {
        public string Name { get; set; }
        public string LockerId { get; set; }
    }
}
