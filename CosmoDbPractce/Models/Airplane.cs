using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDbPractce.Models
{
    public class Airplane
    {
        public string Id { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }   

    }
}
