using HtmlAgilityPack;
using System;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace CuponesTest
{
	internal class Program
	{
		static void Main(string[] args)
		{
			async Task<HtmlDocument> FetchSiteHtml(string url)
			{
				//var req = WebRequest.Create(url);
				//req.Method = "GET";
				//using var resp = req.GetResponse();
				//using var webStream = resp.GetResponseStream();
				//var doc = new HtmlDocument();
				//doc.Load(webStream);

				var httpClient = new HttpClient();
				var html = httpClient.GetStringAsync(url).Result;
				var doc = new HtmlDocument();
				doc.LoadHtml(html);

				return doc;
			}
			string FindStringInBetween(string Text, string FirstString, string LastString)
			{
				string STR = Text;
				string STRFirst = FirstString;
				string STRLast = LastString;
				string FinalString;

				int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
				int Pos2 = STR.IndexOf(LastString);

				FinalString = STR.Substring(Pos1, Pos2 - Pos1);
				return FinalString;
			}
			async Task<IEnumerable<int>> Fetch(string site)
			{
				try
				{
					HtmlDocument doc = await FetchSiteHtml(site);

					var CuponesList = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[1]/*");

					List<Cupon> CList = new List<Cupon>();
					string imageUrl = "";


					for (int i=0;i<CuponesList.Count;i++)
					{	
						string Link = CuponesList[i].GetAttributeValue("href", "");

						var RawImagePage = FetchSiteHtml(CuponesList[i].GetAttributeValue("href", ""));
						string rawImage = RawImagePage.Result.DocumentNode.SelectSingleNode("//*[@id=\"content\"]/div[1]/div/img/@src").OuterHtml;
						Cupon cupon = new Cupon
						{
							Image = FindStringInBetween(rawImage,"src=\"","\" alt"),
							Description = CuponesList[i].GetAttributeValue("title", ""),
							OriginLink = Link
						};
						CList.Add(cupon);
					}

					Console.ReadKey();
					return null;
				}
				catch (Exception ex)
				{
					return null;
				} 
			}
			string site = "https://vkusnotochkamenu.ru/kupon/";
			Fetch(site);
		}
	}
}
//string site = "https://api.zenrows.com/v1/?apikey=af8faf316c099308b06a49bf5da1c979ca447f4a&url=https%3A%2F%2Fvkusnotochkamenu.ru%2Fkupon%2F";

//var req = WebRequest.Create(site);
//req.Method = "GET";
//using var resp = req.GetResponse();
//using var webStream = resp.GetResponseStream();
//var doc = new HtmlDocument();
//doc.Load(webStream);

//var CuponesList = doc.DocumentNode.SelectNodes("//*[@id=\"content\"]/div[1]");