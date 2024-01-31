using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VShop.IdentityServer.Configuration;
using VShop.IdentityServer.Data;

namespace VShop.IdentityServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Registrando o Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            #region String de Conexão - MySQL
            var mySqlConnection = builder
                .Configuration
                .GetConnectionString("DefaultConnection");

            builder.Services
                    .AddDbContext<AppDbContext>(options =>
                    options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
            #endregion

            #region Configurações dos Serviços do IdentityServer
            var builderIdentityServer = builder.Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.EmitStaticAudienceClaim = true;
            }).AddInMemoryIdentityResources(
                           IdentityConfiguration.IdentityResources)
                           .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
                           .AddInMemoryClients(IdentityConfiguration.Clients)
                           .AddAspNetIdentity<ApplicationUser>();

            builderIdentityServer.AddDeveloperSigningCredential();
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
