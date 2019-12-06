using Gotham3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.Models
{
    public class Signalement : Entity
    {
        public string Event_Nature { get; set; }
        public string Sector { get; set; }
        public string Time { get; set; } //HH:MM
        public string Comment { get; set; }
    }
}
