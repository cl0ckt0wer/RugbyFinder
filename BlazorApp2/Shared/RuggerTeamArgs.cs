using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class RuggerTeamArgs
    {
        public Guid RuggerId { get; set; } = Guid.Empty;
        public Guid TeamId { get; set; } = Guid.Empty;
    }
}
