using System;

namespace BlazorApp2.Shared
{
    public class ProfileInfo
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public int DaysSinceLastLogin { get; set; }
        public int DaysSinceCreate { get; set; }
        public string City { get; set; }
        public string TeamName { get; set; }
        public Guid? MyTeamId { get; set; }
    }
}
