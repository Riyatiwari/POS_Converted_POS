using Converted_POS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Converted_POS.Helpers;

namespace Converted_POS.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IBindingService _bindingService;
        protected readonly IRoleService _roleService;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(
            IBindingService bindingService,
            IRoleService roleService,
            ILogger<BaseController> logger)
        {
            _bindingService = bindingService;
            _roleService = roleService;
            _logger = logger;
        }

        protected async Task<bool> HasViewPermission(string moduleName)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetSessionString("user_id") ?? "0");
                return await _roleService.HasViewPermissionAsync(userId, moduleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking view permission for module {ModuleName}", moduleName);
                return false;
            }
        }

        protected async Task<bool> HasEditPermission(string moduleName)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetSessionString("user_id") ?? "0");
                return await _roleService.HasEditPermissionAsync(userId, moduleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking edit permission for module {ModuleName}", moduleName);
                return false;
            }
        }

        protected async Task<bool> HasDeletePermission(string moduleName)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetSessionString("user_id") ?? "0");
                return await _roleService.HasDeletePermissionAsync(userId, moduleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking delete permission for module {ModuleName}", moduleName);
                return false;
            }
        }

        protected async Task<bool> HasCreatePermission(string moduleName)
        {
            try
            {
                var userId = Convert.ToInt32(HttpContext.Session.GetSessionString("user_id") ?? "0");
                return await _roleService.HasCreatePermissionAsync(userId, moduleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking create permission for module {ModuleName}", moduleName);
                return false;
            }
        }

        protected int GetCompanyId()
        {
            return Convert.ToInt32(HttpContext.Session.GetSessionString("cmp_id") ?? "0");
        }

        protected int GetUserId()
        {
            return Convert.ToInt32(HttpContext.Session.GetSessionString("user_id") ?? "0");
        }
    }
} 