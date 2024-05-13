using Cupones.Domain.Entity.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Cupones.Views.Shared.Components.KfcExtraCupone
{
	public class KfcExtraCuponeViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(List<KfcCupon> cupon)
		{
			return View("Default", cupon);
		}
	}
}
