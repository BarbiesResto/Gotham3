﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.domain
{
    public class Sinistre : Entity {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Month { get; set; }
    }
}
