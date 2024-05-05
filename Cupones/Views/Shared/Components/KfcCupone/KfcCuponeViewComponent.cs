using Cupones.Domain.Entity.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace Cupones.Views.Shared.Components.KfcCupone
{
	public class KfcCuponeViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(List<KfcCupon> cupon)
		{
			return View("Default",cupon);
		}
	}
}
