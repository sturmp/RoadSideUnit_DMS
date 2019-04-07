﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DashboardWebApp.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IPAddress IP { get; set; }
        public int Port { get; set; }
        public List<ManagerUser> Users { get; set; }
    }
}
