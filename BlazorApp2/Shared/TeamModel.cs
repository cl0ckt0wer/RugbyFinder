using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class TeamModel
    {
        public string TeamName { get; set; } = string.Empty;
        public string TeamBio { get; set; } = string.Empty;
        public int TeamCityId { get; set; } = 0;
        public Guid TeamId { get; set; } = Guid.Empty;
    }
}
