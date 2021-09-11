using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class InboxItem
    {
        public int MessageCount { get; set; } = 0;
        public DateTime LastMessage { get; set; } = DateTime.MinValue;
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
