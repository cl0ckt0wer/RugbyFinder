using System;

namespace RugbyFinder.Shared
{
    public class MyInfo
    {
        public Guid MyId { get; set; } = Guid.Empty;
        public string MyName { get; set; } = string.Empty;
        public string MyBio { get; set; } = string.Empty;
        public byte[] ProfilePic { get; set; } = Array.Empty<Byte>();
        public TeamModel MyTeam { get; set; } = new TeamModel();
        public string MyImageURI
        {
            get
            {
                if (ProfilePic.Length > 0)
                {
                    return $"data:image/png;base64,{Convert.ToBase64String(ProfilePic)}";
                }
                return string.Empty;
            }
        }
        public CityInfo ClosestCity { get; set; } = new CityInfo();
        public Guid TeamOwned { get; set; } = Guid.Empty;
        public string VideoFile { get; set; } = "";
    }
}
