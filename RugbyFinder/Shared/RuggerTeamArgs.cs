﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class RuggerTeamArgs
    {
        public string Key { get; set; } = "";
        public Guid TeamId { get; set; } = Guid.Empty;
    }
}
