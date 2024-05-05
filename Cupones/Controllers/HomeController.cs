using Cupones.Domain.Entity.Implementations;
using Cupones.Models;
using Cupones.Service.Implementations;
using Cupones.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cupones.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICuponService<KfcCupon> _Kfcservice;

		public HomeController(ICuponService<KfcCupon> KfcService)
		{
			_Kfcservice = KfcService;
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
			var some = _Kfcservice.CurrentGetAll().Result.Data;
			List<KfcCupon> cList = _Kfcservice.CurrentGetAll().Result.Data;
			return View(cList);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
