using Cupones.DAL.Interfaces;
using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.Interfaces;
using Cupones.Domain.Entity.User;
using Cupones.Domain.Enum;
using Cupones.Domain.Response;
using Cupones.Service.Implementations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Service.Interfaces
{
	public abstract class CuponService<ICupon>
	{
		public abstract Task<IBaseResponse<List<ICupon>>> Fetch();
		public abstract Task<IBaseResponse<List<ICupon>>> GetAll();
		public virtual IBaseResponse<List<ICupon>> CurrentGetAll() 
		{
				return new BaseResponse<List<ICupon>>()
				{
					Description = "Новых купонов нет",
					Data = GetAll().Result.Data,
					StatusCode = StatusCode.OK
				};

		}
		public abstract void GetLikes(ICupon cupon);
	}

}

