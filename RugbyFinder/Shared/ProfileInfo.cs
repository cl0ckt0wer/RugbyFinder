using System;

namespace RugbyFinder.Shared
{
    public class ProfileInfo
    {
        public string Name { get; set; } = "";
        public string Bio { get; set; } = "";
        public int DaysSinceLastLogin { get; set; }
        public int DaysSinceCreate { get; set; }
        public string City { get; set; } = "";
        public string TeamName { get; set; } = "";
        public Guid MyTeamId { get; set; } = Guid.Empty;
        public byte[] Pic { get; set; } = new byte[0];
        public string ImageURI
        {
            get
            {
                if (Pic.Length > 0)
                {
                    return $"data:image/png;base64,{Convert.ToBase64String(Pic)}";
                }
                return string.Empty;
            }
        }

    }
}
