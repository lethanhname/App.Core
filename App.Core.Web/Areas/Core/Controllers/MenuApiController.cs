using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.CoreLib.EF.Data;
using App.CoreLib.EF.Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using App.CoreLib.Extensions;
using App.Core.Contract.Security;
using App.Core.Web.ViewModels.Shared;
namespace App.Core.Web.Areas.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policy = Policies.HasActionPermission)]
    [Area("Core")]
    public class MenuApiController : Controller
    {
        public MenuApiController() { }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var menus = new MenuViewModelFactory(this.Url, this.HttpContext).Create();
            return Ok(menus.MenuItems);
        }
    }
}