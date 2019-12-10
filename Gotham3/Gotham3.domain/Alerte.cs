using System;
using System.Collections.Generic;
using System.Text;

namespace Gotham3.domain
{ 

    public class Alerte : Entity
    {
        public string Event_Nature { get; set; }
        public string Sector { get; set; }
        public string Risk { get; set; }
        public string Ressource { get; set; }
        public string Advice { get; set; }
    }
}
