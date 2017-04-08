using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBcBackend.Models
{
    public class MassEmail
    {
        public long Emid { get; set; }
        public DateTime Emcreateddate { get; set; }
        public string Emauthor { get; set; }
        public string Emtext { get; set; }
    }
}
