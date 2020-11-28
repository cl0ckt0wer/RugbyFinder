using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class ProfileInfo
    {
        public string Name { get; set; }
        public string Bio { get; set;}
        public int DaysSinceLastLogin { get; set; }
        public int DaysSinceCreate { get; set; }
        public String City { get; set; }
    }
}
