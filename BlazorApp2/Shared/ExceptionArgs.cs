using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class ExceptionArgs
    {
        public string Myexception { get; set; } = "";
        public Guid MyGuid { get; set; } = Guid.Empty;

    }
}
