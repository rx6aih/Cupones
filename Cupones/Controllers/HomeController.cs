using Cupones.DAL.Repositories;
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
		private readonly CuponService<KfcCupon> _Kfcservice;

		public HomeController(CuponService<KfcCupon> KfcService)
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
			List<KfcCupon> cList = _Kfcservice.CurrentGetAll().Data;
			return View(cList);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
