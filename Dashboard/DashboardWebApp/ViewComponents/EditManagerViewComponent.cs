﻿using DashboardWebApp.Models;
using DashboardWebApp.ViewModels.Manager;
using DashboardWebApp.ViewModels.RSU;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashboardWebApp.ViewComponents
{
    public class EditManagerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ManagerEditModel manager)
        {
            await Task.Delay(0);
            return View("Edit", manager);
        }
    }
}
