using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class RuggerMessagePostArgs
    {
        public string MyKey { get; set; } = "";
        public Guid TheirGuid { get; set; } = Guid.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
