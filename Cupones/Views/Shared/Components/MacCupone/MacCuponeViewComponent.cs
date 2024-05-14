using Cupones.Domain.Entity.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Cupones.Views.Shared.Components.MacCupone
{
    public class MacCuponeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<MacCupon> cupon)
        {
            return View("Default", cupon);
        }
    }
}
