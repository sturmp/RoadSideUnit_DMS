﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SNMPManager.Core.Enumerations;

namespace SNMPManager.Core.Entities
{
    public class ManagerLog : Log
    {
        public ManagerLog()
        {
                
        }

        public ManagerLog(DateTime timestamp, LogType type, string message)
        {
            TimeStamp = timestamp;
            Type = type;
            Message = message;
        }
    }
}
