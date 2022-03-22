using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagemenSystem_Ims.Auth;
using InventoryManagemenSystem_Ims.Implementations.Repositories;
using InventoryManagemenSystem_Ims.Implementations.Services;
using InventoryManagemenSystem_Ims.IMS_DbContext;
using InventoryManagemenSystem_Ims.Interfaces.Repositories;
using InventoryManagemenSystem_Ims.Interfaces.Services;
using InventoryManagemenSystem_Ims.SendMail;
using MailKit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagemenSystem_Ims
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ImsContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("ConnectionContext")));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ISalesManagerRepository, SalesManagerRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockKeeperRepository, StockKeeperRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IShopManagerRepository, ShopManagerRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISalesRepository, SalesRepository>();
            services.AddScoped<IReturnGoodsRepository, ReturnGoodsRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IShopManagerService, ShopManagerService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IStockKeeperService, StockKeeperService>();
            services.AddScoped<ISalesManagerService, SalesManagerService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISalesItemService, SalesItemService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IMailMessage, MailMessage>();
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/user/login";
                    config.Cookie.Name = "InventoryManagemenSystem_Ims";
                    config.LogoutPath = "/user/logout";
                    
                });
            services.AddAuthorization();

            services.AddControllersWithViews();
            
            //var key = "This is our key that we are using to authorize our user";
            
            // services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
            //
            // services.AddAuthentication(x =>
            //     {
            //         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     })
            //     .AddJwtBearer(x =>
            //     {
            //         x.RequireHttpsMetadata = false;
            //         x.SaveToken = true;
            //         x.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuerSigningKey = true,
            //             IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            //             ValidateIssuer = false,
            //             ValidateAudience = false
            //         };
            //
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}