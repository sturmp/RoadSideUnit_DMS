﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SNMPManager.Core.Entities;
using SNMPManager.Core.Enumerations;
using SNMPManager.Core.Interfaces;
using SNMPManager.Core.Exceptions;
using Microsoft.EntityFrameworkCore.Internal;
using SNMPManager.Infrastructure;

namespace SNMPManager.Persistence
{
    public class ContextService : IContextService 
    {
        private readonly ManagerContext _managerContext;
        private readonly ManagerLogger _logger;

        public ContextService(ManagerContext managerContext)
        {
            _managerContext = managerContext;
            _logger = new ManagerLogger(this);
        }



        #region RSU service functions
        public bool AddRSU(RSU rsu)
        {
            if (_managerContext.RSUs.Any(r => r.Id == rsu.Id))
                return false;

            _managerContext.Add(rsu);
            _managerContext.SaveChanges();

            return true;
        }

        public RSU GetRSU(int rsuId)
        {
            return _managerContext.RSUs.Find(rsuId);
        }

        public ICollection<RSU> GetRSU()
        {
            return _managerContext.RSUs.ToArray();
        }

        public bool RemoveRSU(int rsuId)
        {
            var rsu = _managerContext.RSUs.Find(rsuId);
            if (rsu == null)
                return false;

            _managerContext.Remove(rsu);
            _managerContext.SaveChanges();
            return true;
        }

        public bool UpdateRSU(RSU rsu)
        {
            var rsu_mod = _managerContext.RSUs.Find(rsu.Id);
            if (rsu_mod == null)
                return false;

            _managerContext.Update(rsu);
            _managerContext.SaveChanges();
            return true;
        }
        #endregion


        #region User service functions
        public bool AddUser(User user)
        {
            if (_managerContext.Users.Any(u=> u.Id == user.Id))
                return false;

            _managerContext.Add(user);
            _managerContext.SaveChanges();

            return true;
        }

        public User GetUser(int userId)
        {
            return _managerContext.Users.Find(userId);
        }

        public ICollection<User> GetUser()
        {
            return _managerContext.Users.ToArray();
        }

        public bool UpdateUser(User user)
        {
            var user_mod = _managerContext.RSUs.Find(user.Id);
            if (user_mod == null)
                return false;

            _managerContext.Update(user);
            _managerContext.SaveChanges();
            return true;
        }

        public bool RemoveUser(int userId)
        {
            var user = _managerContext.RSUs.Find(userId);
            if (user == null)
                return false;

            _managerContext.Remove(user);
            _managerContext.SaveChanges();
            return true;
        }

        public User AuthenticateUser(string userName, string token)
        {
            var user = _managerContext.Users.Find(userName);
            if (user == null)
            {
                _logger.LogAuthentication(userName, false);
                return null;
            }

            if (user.Token != token)
            {
                _logger.LogAuthentication(userName, false);
                throw new AuthenticationFailed(userName);
            }

            _logger.LogAuthentication(userName, true);
            return user;
        }

        public bool AuthorizeUser(string userName, string token, ManagerTask task)
        {
            var user = AuthenticateUser(userName, token);
            if (user == null)
            {
                _logger.LogAuthorization(userName, task, false);
                return false;
            }

            var managertasks = user.Role.ManagerTasks;
            if (managertasks == null)
            {
                _logger.LogAuthorization(userName, task, false);
                throw new AuthorizationFailed(userName, task);
            }

            if (!managertasks.Any(t => t.ToString() == task.ToString()))
            {
                _logger.LogAuthorization(userName, task, false);
                throw new AuthorizationFailed(userName, task);
            }

            _logger.LogAuthorization(userName, task, true);
            return true;
        }
        #endregion

        #region Role service functions
        public bool AddRole(Role role)
        {
            if (_managerContext.Roles.Any(r => r.Id == role.Id))
                return false;

            _managerContext.Add(role);
            _managerContext.SaveChanges();

            return true;
        }

        public bool RemoveRole(int roleId)
        {
            var role = _managerContext.Roles.Find(roleId);
            if (role == null)
                return false;

            _managerContext.Remove(role);
            _managerContext.SaveChanges();
            return true;
        }

        public bool UpdateRole(Role role)
        {
            var role_mod = _managerContext.Roles.Find(role.Id);
            if (role_mod == null)
                return false;

            _managerContext.Update(role);
            _managerContext.SaveChanges();
            return true;
        }

        public Role GetRole(int roleId)
        {
            return _managerContext.Roles.Find(roleId);
        }

        public ICollection<Role> GetRole()
        {
            return _managerContext.Roles.ToArray();
        }
        #endregion

        #region Token service functions
        public void AddToken(Token token)
        {
            throw new NotImplementedException();
        }

        public Token GetToken(int tokenId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Token> GetToken()
        {
            throw new NotImplementedException();
        }

        public void UpdateToken(Token token)
        {
            throw new NotImplementedException();
        }

        public void RemoveToken(int tokenId)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Log service functions
        public void AddManagerLog(ManagerLog log)
        {
            _managerContext.Add(log);
            _managerContext.SaveChanges();
        }

        public void AddTrapLog(TrapLog log)
        {
            _managerContext.Add(log);
            _managerContext.SaveChanges();
        }


        public IEnumerable<ManagerLog> GetManagerLogs()
        {
            return _managerContext.ManagerLogs.ToArray();
        }

        public IEnumerable<ManagerLog> GetManagerLogs(DateTime from, DateTime to)
        {
            return _managerContext.ManagerLogs
                                    .Where( log => log.TimeStamp >= from
                                                    && log.TimeStamp <= to)
                                    .ToArray();
        }

        public IEnumerable<ManagerLog> GetManagerLogs(LogLevel logLevel, DateTime from, DateTime to)
        {
            return _managerContext.ManagerLogs
                                    .Where(log => log.TimeStamp >= from
                                                   && log.TimeStamp <= to
                                                   && log.Type == logLevel)
                                    .ToArray();
        }

        public IEnumerable<TrapLog> GetTrapLogs()
        {
            return _managerContext.TrapLogs.ToArray();
        }

        public IEnumerable<TrapLog> GetTrapLogs(DateTime from, DateTime to)
        {
            return _managerContext.TrapLogs
                                    .Where(log => log.TimeStamp >= from
                                                   && log.TimeStamp <= to)
                                    .ToArray();
        }

        public IEnumerable<TrapLog> GetTrapLogs(LogLevel logLevel, DateTime from, DateTime to)
        {
            return _managerContext.TrapLogs
                                    .Where(log => log.TimeStamp >= from
                                                   && log.TimeStamp <= to
                                                   && log.Type == logLevel)
                                    .ToArray();
        }

        public IEnumerable<TrapLog> GetTrapLogs(int rsuId, DateTime from, DateTime to)
        {
            return _managerContext.TrapLogs
                                    .Where(log => log.TimeStamp >= from
                                                   && log.TimeStamp <= to
                                                   && log.SourceRSU == rsuId)
                                    .ToArray();
        }

        public IEnumerable<TrapLog> GetTrapLogs(int rsuId)
        {
            return _managerContext.TrapLogs
                                    .Where(log => log.SourceRSU == rsuId)
                                    .ToArray();
        }

        #endregion
    }
}