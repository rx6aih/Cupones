using Cupones.DAL.Interfaces;
using Cupones.DAL.Repositories;
using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.Interfaces;
using Cupones.Domain.Entity.User;
using Cupones.Models;
using Cupones.Service.Implementations;
using Cupones.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Cupones.Controllers
{
	public class HomeController : Controller
	{
		private readonly CuponService<KfcCupon> _Kfcservice;
		private readonly CuponService<MacCupon> _Macservice;
		int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

		public HomeController(CuponService<KfcCupon> KfcService, CuponService<MacCupon> MacService)
		{
			_Kfcservice = KfcService;
			_Macservice = MacService;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Kfc()
		{
			List<KfcCupon> cList = _Kfcservice.CurrentGetAll().Data;
			return View(cList);
		}

		public IActionResult Mac()
		{
			List<MacCupon> cList = _Macservice.CurrentGetAll().Data;
			return View(cList);
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
