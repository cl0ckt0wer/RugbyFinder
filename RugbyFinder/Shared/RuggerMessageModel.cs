using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class RuggerMessageModel
    {
        public Guid From { get; set; } = Guid.Empty;
        public Guid To { get; set; } = Guid.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime sentDate { get; set; } = DateTime.MinValue;
    }
  
}
