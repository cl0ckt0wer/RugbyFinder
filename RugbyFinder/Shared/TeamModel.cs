using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class TeamModel
    {
        public string TeamName { get; set; } = string.Empty;
        public string TeamBio { get; set; } = string.Empty;
        public Guid TeamId { get; set; } = Guid.Empty;
        public CityInfo City { get; set; } = new CityInfo();
        public byte[] TeamPic { get; set; } = new byte[0];
        public Guid TeamOwner { get; set; } = Guid.Empty;
        public int Playercount { get; set; }
        public string TeamImageURI { get
            {
                if(TeamPic.Length > 0)
                {
                    return $"data:image/png;base64,{Convert.ToBase64String(TeamPic)}";
                }
                return string.Empty;
            } }
    }
}
