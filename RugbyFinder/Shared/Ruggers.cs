using System.Collections.Generic;

namespace RugbyFinder.Shared
{
    public class Ruggers
    {
        public Ruggers()
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
