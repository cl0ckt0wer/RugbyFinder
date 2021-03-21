using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared.Model.Return
{
    public class KeyGenReturnModel
    {
        public string Key { get; set; }
        public Guid MyGuid { get; set; } = Guid.Empty;
    }
}
