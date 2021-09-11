using System;

namespace RugbyFinder.Shared
{
    public class ClosestRuggers
    {
        public string Name { get; set; }
        public int LocationOrder { get; set; }
        public Guid guid { get; set; }
        public string Bio { get; set; }
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
        public string VideoFile { get; set; } = "";
    }
}
