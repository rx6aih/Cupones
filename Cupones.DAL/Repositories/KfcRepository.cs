using Cupones.DAL.EF;
using Cupones.DAL.Interfaces;
using Cupones.Domain.Entity.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.DAL.Repositories
{
	public class KfcRepository : IBaseRepository<KfcCupon>
	{
		private readonly AppDBContext _appDbContext;

		public KfcRepository(AppDBContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task Create(KfcCupon entity)
		{
			await _appDbContext.KfcCupons.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public IQueryable<KfcCupon> GetAll()
		{
			return _appDbContext.KfcCupons;
		}

		public async Task Delete(KfcCupon entity)
		{
			_appDbContext.KfcCupons.Remove(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<KfcCupon> Update(KfcCupon entity)
		{
			_appDbContext.KfcCupons.Update(entity);
			await _appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}
