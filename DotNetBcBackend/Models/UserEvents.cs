using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBcBackend.Models
{
    public class UserEvents
    {
        public long Usereventid { get; set; }
        public string Userid { get; set; }
        public string Evid { get; set; }
        public Boolean Attending { get; set; }
    }
}
