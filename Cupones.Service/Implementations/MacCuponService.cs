using Cupones.DAL.Interfaces;
using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Enum;
using Cupones.Domain.Response;
using Cupones.Service.Interfaces;
using Cupones.Service.Utility;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Service.Implementations
{
	public class MacCuponService : CuponService<MacCupon>
	{
		//Сайт для запроса html документа
		string site = "https://vkusnotochkamenu.ru/kupon/";
		string cuponImagePage = "https://vkusnotochkamenu.ru/info/";

		//экземпляр репозитория и логера
		private readonly IBaseRepository<MacCupon> _repository;
		private ILogger<MacCuponService> _logger;

		public MacCuponService(IBaseRepository<MacCupon> repository, ILogger<MacCuponService> logger)
		{
			this._repository = repository;
			this._logger = logger;
		}

		//мектод для получения списка купонов с сайта. Возвращает ответ в виде статуса и данных
		public  override async Task<IBaseResponse<List<MacCupon>>> Fetch()
		{
			HtmlUtility htmlUtility = new HtmlUtility();
			HtmlDocument doc = await htmlUtility.FetchSiteHtml(site);

			var RawCuponesList = doc.DocumentNode.SelectNodes("/html/body/div[2]/div/div/div/div[1]/*");

			List<MacCupon> FinalCuponesList = new List<MacCupon>();

			for (int i = 0; i < RawCuponesList.Count; i++)
			{
				var RawImagePage = htmlUtility
					.FetchSiteHtml(RawCuponesList[i]
					.GetAttributeValue("href", ""));

				string rawImage = RawImagePage.
					Result.
					DocumentNode.
					SelectSingleNode("//*[@id=\"content\"]/div[1]/div/img/@src").
					OuterHtml;

				MacCupon cupon = new MacCupon
				{
					Description = RawCuponesList[i]
					.GetAttributeValue("title", ""),

					OriginLink = RawCuponesList[i].GetAttributeValue("href", ""),

					Image = htmlUtility.FindStringInBetween(rawImage, "src=\"", "\" alt"),

					UpdatedDate = DateTime.Today
				};
				_repository.Create(cupon);
				FinalCuponesList.Add(cupon);
			}

			return new BaseResponse<List<MacCupon>>()
			{
				Data = FinalCuponesList,
				StatusCode = StatusCode.OK
			};
		}

		public override async Task<IBaseResponse<List<MacCupon>>> GetAll()
		{
			try
			{
				var cupons = await _repository.GetAll()
					.Select(x => new MacCupon()
					{
						Description = x.Description,
						Id = x.Id,
						Image = x.Image,
						OriginLink = x.OriginLink
					})
					.ToListAsync();
				if (cupons == null)
				{
					return new BaseResponse<List<MacCupon>>()
					{
						StatusCode = Domain.Enum.StatusCode.InternalServerError,
						Description = "Купоны не найдены"
					};
				}

				return new BaseResponse<List<MacCupon>>()
				{
					Data = cupons,
					StatusCode = Domain.Enum.StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"[MacCuponService.GetAll]: {ex.Message}");
				return new BaseResponse<List<MacCupon>>()
				{
					Description = $"{ex.Message.ToString()}",
					StatusCode = Domain.Enum.StatusCode.InternalServerError
				};
			}
		}

		//метод проверяет совпадает ли дата обновления купонов с сегодняшней,
		//и если нет - то удаляет все строки и заполняет их новыми данными
		public override IBaseResponse<List<MacCupon>> CurrentGetAll()
		{
            var cupons = _repository.GetAll().Where(x => x.UpdatedDate != DateTime.Today.Date).ToList().Count;
            if (cupons != 0)
            {
                return new BaseResponse<List<MacCupon>>()
                {
                    Description = "Новых купонов нет",
                    Data = GetAll().Result.Data,
                    StatusCode = StatusCode.OK
                };
            }
            else
                return new BaseResponse<List<MacCupon>>()
                {
                    Description = "Новые купоны возможно есть",
                    Data = Fetch().Result.Data,
                    StatusCode = Domain.Enum.StatusCode.Updated
                };
        }

		public override void GetLikes(MacCupon cupon)
		{
			throw new NotImplementedException();
		}
	}
}
