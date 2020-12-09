using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class RuggerMessageModel
    {
        public Guid Other { get; set; } = Guid.Empty;

        public List<string> MyMessage { get; set; } = new List<string>();
    }
}
