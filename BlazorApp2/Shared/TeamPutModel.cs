using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class TeamPutModel : TeamModel
    {
        public Guid RuggerOwner { get; set; } = Guid.Empty;
    }
}
