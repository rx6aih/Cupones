using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.Interfaces;
using Cupones.Service.Interfaces;
using Cupones.Service.Utility;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Service.Implementations
{
		//public class BKCuponService : ICuponService<BKCupon>
		//{
		//	string site = "https://api.zenrows.com/v1/?apikey=af8faf316c099308b06a49bf5da1c979ca447f4a&url=https%3A%2F%2Frostics.ru%2Fcoupons";
		//	string cuponImagePage = "https://api.zenrows.com/v1/?apikey=af8faf316c099308b06a49bf5da1c979ca447f4a&url=https%3A%2F%2Frostics.ru%2Fproduct%2F";
		//	public async Task<List<BKCupon>> Fetch()
		//	{
		//		HtmlUtility htmlUtility = new HtmlUtility();
		//		HtmlDocument doc = await htmlUtility.FetchSiteHtml(site);

		//		var RawCuponesList = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div/div[2]/div[2]/*");

		//		List<BKCupon> FinalCuponesList = new List<BKCupon>();

		//		for (int i = 0; i < RawCuponesList.Count; i++)
		//		{
		//			int is5050 = Convert.ToInt32(RawCuponesList[i]
		//				.SelectSingleNode(".//a/div[2]/text()")
		//				.InnerText);

		//			var RawImagePage = htmlUtility
		//				.FetchSiteHtml(cuponImagePage + RawCuponesList[i]
		//				.SelectSingleNode(".//a/div[2]/text()")
		//				.InnerText);

		//			string rawImage = RawImagePage.Result.DocumentNode
		//				.SelectSingleNode("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div/div[1]/div[2]/img/@src")
		//				.OuterHtml;

		//			BKCupon cupon = new BKCupon
		//			{
		//				Code = Convert.ToInt32(RawCuponesList[i]
		//				.SelectSingleNode(".//a/div[2]/text()")
		//				.InnerText),

		//				Description = RawCuponesList[i]
		//				.SelectSingleNode(".//a/div[3]/text()")
		//				.InnerText,


		//				OriginLink = "https://rostics.ru" + RawCuponesList[i]
		//				.SelectSingleNode(".//a")
		//				.GetAttributeValue("href", ""),

		//				Image = htmlUtility.FindStringInBetween(rawImage, "src=\"", "\" alt")
		//			};
		//			FinalCuponesList.Add(cupon);
		//		}

		//		return FinalCuponesList;
		//	}

		//}
	}