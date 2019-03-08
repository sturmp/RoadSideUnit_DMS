﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SNMPManager.Core.Enumerations;

namespace SNMPManager.Core.Entities
{
    public class TrapLog : Log
    {
        public int SourceRSU { get; set; }
    }
}
