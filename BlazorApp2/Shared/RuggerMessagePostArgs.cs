using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class RuggerMessagePostArgs
    {
        public Guid MyGuid { get; set; } = Guid.Empty;
        public Guid TheirGuid { get; set; } = Guid.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
