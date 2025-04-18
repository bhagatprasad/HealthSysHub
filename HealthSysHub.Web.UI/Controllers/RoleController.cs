﻿using AspNetCoreHero.ToastNotification.Abstractions;
using HealthSysHub.Web.UI.Interfaces;
using HealthSysHub.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthSysHub.Web.UI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyfService;

        public RoleController(IRoleService roleService,
            INotyfService notyfService)
        {
            _roleService = roleService;
            _notyfService = notyfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateRole([FromBody] Role role)
        {
            try
            {
                var response = await _roleService.InsertOrUpdateRoleAsync(role);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        public async Task<IActionResult> FetchRoles()
        {
            try
            {
                var responce = await _roleService.GetRolesAsync();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchUserRoles()
        {
            try
            {
                var responce = await _roleService.GetRolesAsync();
                return Json(new { data = responce });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
