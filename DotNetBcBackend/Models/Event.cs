using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBcBackend.Models
{
    public class Event
    {
        public long Evid { get; set; }
        public string Evday { get; set; }
        public string Evdate { get; set; }
        public string Evtime { get; set; }
        public string Evloc { get; set; }
        public string Evbrief { get; set; }
        public string Evbriefdesc { get; set; }
        public string Evdetail { get; set; }
        public DateTime Evpubdate { get; set; }
    }
}
