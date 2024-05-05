using Cupones.Domain.Entity.Interfaces;
using Cupones.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.Service.Interfaces
{
	public interface ICuponService<ICupon>
	{
		Task <IBaseResponse<List<ICupon>>> Fetch();
		Task<IBaseResponse<List<ICupon>>> GetAll();
		Task<IBaseResponse<List<ICupon>>> CurrentGetAll();
	}
}
