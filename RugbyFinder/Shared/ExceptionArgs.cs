﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugbyFinder.Shared
{
    public class ExceptionArgs
    {
        public string Myexception { get; set; } = "";
        public string Key { get; set; } = "";
        public Guid MyGuid { get; set; } = Guid.Empty;
    }
}
