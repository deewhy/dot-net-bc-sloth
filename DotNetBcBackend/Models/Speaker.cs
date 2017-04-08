using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetBcBackend.Models
{
    public class Speaker
    {
        public long Speakerid { get; set; }
        public string Speakerpic { get; set; }
        public string Speakername { get; set; }
        public string Speakeremail { get; set; }
        public string Speakerphone { get; set; }
        public string Speakerspec { get; set; }
        public string Speakerbio { get; set; }
    }
}
