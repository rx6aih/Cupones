using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.Interfaces;
using Cupones.Domain.Response;
using Cupones.Service.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cupones.Service.Utility;
using Cupones.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Cupones.Domain.Enum;
namespace Cupones.Service.Implementations
{
	public class KfcCuponService : ICuponService<KfcCupon>
	{
		//Сайт для запроса html документа
		string site = "https://rostics.ru/coupons";
        string cuponImagePage = "https://rostics.ru/product/";

		//экземпляр репозитория и логера
		private readonly IBaseRepository<KfcCupon> _repository;
		private ILogger<KfcCuponService> _logger;

		public KfcCuponService(IBaseRepository<KfcCupon> repository, ILogger<KfcCuponService> logger)
		{
			this._repository = repository;
			this._logger = logger;
		}

		//мектод для получения списка купонов с сайта. Возвращает ответ в виде статуса и данных
		public async Task<IBaseResponse<List<KfcCupon>>> Fetch()
		{
			HtmlUtility htmlUtility = new HtmlUtility();
			HtmlDocument doc = await htmlUtility.FetchSiteHtml(site);

			var RawCuponesList = doc.DocumentNode.SelectNodes("//*[@id=\"root\"]/div/div[2]/div[2]/*");

			List<KfcCupon> FinalCuponesList = new List<KfcCupon>();

			for (int i = 0; i < RawCuponesList.Count; i++)
			{
				//проверка на однодневный купон
				int is5050 = Convert.ToInt32(RawCuponesList[i]
					.SelectSingleNode(".//a/div[2]/text()")
					.InnerText);
				
				//получение ссылки на страницу купона с изображением
				var RawImagePage = htmlUtility
					.FetchSiteHtml(cuponImagePage + RawCuponesList[i]
					.SelectSingleNode(".//a/div[2]/text()")
					.InnerText);
				
				//получение изображения
				string rawImage = RawImagePage.Result.DocumentNode
					.SelectSingleNode("//*[@id=\"root\"]/div/div[2]/div[1]/div[1]/div/div[1]/div[2]/img/@src")
					.OuterHtml;

				KfcCupon cupon = new KfcCupon
				{
					Code = Convert.ToInt32(RawCuponesList[i]
					.SelectSingleNode(".//a/div[2]/text()")
					.InnerText),

					Description = RawCuponesList[i]
					.SelectSingleNode(".//a/div[3]/text()")
					.InnerText,

					IsExpiring = is5050 == 5050 ? true : false,

					OriginLink = "https://rostics.ru" + RawCuponesList[i]
					.SelectSingleNode(".//a")
					.GetAttributeValue("href", ""),

					Image = htmlUtility.FindStringInBetween(rawImage, "src=\"", "\" alt"),

					UpdatedDate = DateTime.Today
				};
				_repository.Create(cupon);
				FinalCuponesList.Add(cupon);
			}

			return new BaseResponse<List<KfcCupon>>()
			{
				Data = FinalCuponesList,
				StatusCode = StatusCode.OK
			};
		}

		public async Task<IBaseResponse<List<KfcCupon>>> GetAll()
		{
			try
			{
				var cupons = await _repository.GetAll()
					.Select(x => new KfcCupon()
					{
						Code = x.Code,
						Description = x.Description,
						Id = x.Id,
						Image = x.Image,
						IsExpiring = x.IsExpiring,
						OriginLink = x.OriginLink,
					})
					.ToListAsync();
				if (cupons.Count==0)
				{
					return new BaseResponse<List<KfcCupon>>()
					{
						StatusCode = Domain.Enum.StatusCode.InternalServerError,
						Description = "Купоны не найдены"
					};
				}

				return new BaseResponse<List<KfcCupon>>()
				{
					Data = cupons,
					StatusCode = Domain.Enum.StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[KfcCuponService.GetAll]: {ex.Message}");
				return new BaseResponse<List<KfcCupon>>()
				{
					Description = $"{ex.Message.ToString()}",
					StatusCode = Domain.Enum.StatusCode.InternalServerError
				};
			}
		}

		//метод проверяет совпадает ли дата обновления купонов с сегодняшней,
		//и если нет - то удаляет все строки и заполняет их новыми данными
		public async Task<IBaseResponse<List<KfcCupon>>> CurrentGetAll()
		{
			var someCupons = _repository.GetAll().ToList();

			var cupons = _repository.GetAll().Where(x => x.UpdatedDate != DateTime.Today).ToList().Count;
            if (cupons==0)
			{
				return new BaseResponse<List<KfcCupon>>()
				{
					Description = "Новых купонов нет",
					Data = GetAll().Result.Data,
					StatusCode = StatusCode.OK
				};
			}
			else
				return new BaseResponse<List<KfcCupon>>()
				{
					Description = "Новые купоны возможно есть",
					Data = Fetch().Result.Data,
					StatusCode = Domain.Enum.StatusCode.Updated
				};
		}
	}
}
