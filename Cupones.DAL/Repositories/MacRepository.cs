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
	public class MacRepository : IBaseRepository<MacCupon>
	{
		private readonly AppDBContext _appDbContext;

		public MacRepository(AppDBContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task Create(MacCupon entity)
		{
			await _appDbContext.MacCupons.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public IQueryable<MacCupon> GetAll()
		{
			return _appDbContext.MacCupons;
		}

		public async Task Delete(MacCupon entity)
		{
			_appDbContext.MacCupons.Remove(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<MacCupon> Update(MacCupon entity)
		{
			_appDbContext.MacCupons.Update(entity);
			await _appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}
