using System;

namespace BlazorApp2.Shared
{
    public class MyInfo
    {
        public string MyName { get; set; }
        public string MyBio { get; set; }
        public string TeamName { get; set; }
        public Guid? TeamId { get; set; }
        public byte[] ProfilePic { get; set; } = new byte[0];
    }
}
