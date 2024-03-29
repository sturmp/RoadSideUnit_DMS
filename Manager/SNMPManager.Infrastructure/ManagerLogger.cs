﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNMPManager.Core.Entities;
using SNMPManager.Core.Enumerations;
using SNMPManager.Core.Interfaces;

namespace SNMPManager.Infrastructure
{
    public class ManagerLogger : ILogger
    {
        private IContextService _contextService;

        public ManagerLogger(IContextService ContextService)
        {
            _contextService = ContextService;
        }
        public void Log(LogType level, string message)
        {
            ManagerLog log = new ManagerLog(DateTime.UtcNow, level, message);

            _contextService.AddManagerLog(log);
        }

        public void LogAPICall(string userName, ManagerOperation managerOperation)
        {
            Log(LogType.APICALL, $"[{userName}] requested [{managerOperation.ToString()}] task.");
        }

        public void LogAuthentication(string userName, bool success)
        {
            Log(LogType.SECURITY, $"Authentication for [{userName}] was [{GetSuccessString(success)}]!");
        }

        public void LogAuthorization(string userName, bool success)
        {
            Log(LogType.SECURITY, $"Authorization for [{userName}] was [{GetSuccessString(success)}]!");
        }

        public void LogDBOperation(string userName, DBOperation Operation)
        {
            Log(LogType.DB, $"[{Operation.ToString()}] operation executed for [{userName}].");
        }

        private string GetSuccessString(bool success)
        {
            return success ? "successful!" : "unsuccessful!";
        }
    }
}
