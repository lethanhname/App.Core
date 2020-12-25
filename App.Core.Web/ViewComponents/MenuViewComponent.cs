using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using App.Core.Web.ViewModels.Shared;

namespace App.Core.Web.ViewComponents
{
  public class MenuViewComponent : ViewComponent
  {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult((IViewComponentResult)View(new MenuViewModelFactory(Url, HttpContext).Create()));
        }
  }
}