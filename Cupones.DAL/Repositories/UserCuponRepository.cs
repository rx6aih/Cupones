using Cupones.DAL.EF;
using Cupones.DAL.Interfaces;
using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.DAL.Repositories
{
	public class UserCuponRepository : IBaseRepository<UserCupon>
	{
		private readonly AppDBContext _appDbContext;

		public UserCuponRepository(AppDBContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task Create(UserCupon entity)
		{
			await _appDbContext.UserCupons.AddAsync(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public IQueryable<UserCupon> GetAll()
		{
			return _appDbContext.UserCupons;
		}

		public async Task Delete(UserCupon entity)
		{
			_appDbContext.UserCupons.Remove(entity);
			await _appDbContext.SaveChangesAsync();
		}

		public async Task<UserCupon> Update(UserCupon entity)
		{
			_appDbContext.UserCupons.Update(entity);
			await _appDbContext.SaveChangesAsync();

			return entity;
		}
	}
}
