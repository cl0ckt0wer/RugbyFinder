using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class Ruggers
    {
        public Ruggers ()
        {
            ClosestUsers = new List<Rugger>();
        }

        public List<Rugger> ClosestUsers { get; set; }
    }
    public class Rugger
    {
        public string UserName { get; set; }
        public int Distance { get; set; }
        public bool Active { get; set; }
    }
}
