using Cupones.DAL.EF;
using Cupones.DAL.Interfaces;
using Cupones.DAL.Repositories;
using Cupones.Domain.Entity.Implementations;
using Cupones.Service.Implementations;
using Cupones.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Cupones
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddScoped<IBaseRepository<KfcCupon>, KfcRepository>();
            builder.Services.AddScoped<ICuponService<KfcCupon>, KfcCuponService>();
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

			var connectionString = builder.Configuration.GetConnectionString("NPGSQL");
			builder.Services.AddDbContext<AppDBContext>(options =>
			{
				options.UseNpgsql(connectionString);
			});

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => options.LoginPath = "/account");
			builder.Services.AddAuthorization();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
