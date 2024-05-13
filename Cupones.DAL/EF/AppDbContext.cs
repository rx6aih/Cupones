using Cupones.Domain.Entity.Implementations;
using Cupones.Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cupones.DAL.EF
{
    public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
		public DbSet<User> Users { get; set; }
		public DbSet<UserCupon> UserCupons { get; set; }
		public DbSet<KfcCupon> KfcCupons { get; set; }
		public DbSet<MacCupon> MacCupons { get; set; }
	}
}
