using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSFinder.Models
{
    public class Message
    {
        public DateTime? Date { get; set; }
        public string Detail { get; set; }
        public string from { get; set; }
        public string to { get; set; }
    }
}
