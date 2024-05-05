using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Service.Utility
{
	public class HtmlUtility
	{
		public async Task<HtmlDocument> FetchSiteHtml(string url)
		{
			var httpClient = new HttpClient();
			var html = httpClient.GetStringAsync(url).Result;
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			return doc;
		}
		public string FindStringInBetween(string Text, string FirstString, string LastString)
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
	}
}
