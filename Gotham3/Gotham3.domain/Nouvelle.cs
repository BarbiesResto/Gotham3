using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.domain
{
    public class Nouvelle : Entity
    {
        public string Title { get; set; }
        public string Text_Desc { get; set; }
        public string Link_Media { get; set; }
    }
}
