using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class CityInfoArgs
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
        public Guid MyGuid { get; set; }
    }
}
