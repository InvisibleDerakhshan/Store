using Bugeto_Store.Application.Interfaces.FacadPatterns;
using Bugeto_Store.Application.Services.Carts;
using Bugeto_Store.Application.Services.Common.Queries.GetCategory;
using Bugeto_Store.Application.Services.Common.Queries.GetHomePageImages;
using Bugeto_Store.Application.Services.Common.Queries.GetMenuItem;
using Bugeto_Store.Application.Services.Common.Queries.GetSlider;
using Bugeto_Store.Application.Services.Finances.Commands.AddRequestPay;
using Bugeto_Store.Application.Services.HomePages.AddHomePageImages;
using Bugeto_Store.Application.Services.HomePages.AddNewSlider;
using Bugeto_Store.Application.Services.Products.FacadPattern;
using Bugeto_Store.Application.Services.Users.Commands.EditUser;
using Bugeto_Store.Application.Services.Users.Commands.RegisterUser;
using Bugeto_Store.Application.Services.Users.Commands.RemoveUser;
using Bugeto_Store.Application.Services.Users.Commands.UserLogin;
using Bugeto_Store.Application.Services.Users.Commands.UserStatusChange;
using Bugeto_Store.Application.Services.Users.Queries.GetRoles;
using Bugeto_Store.Application.Services.Users.Queries.GetUsers;
using Bugeto_Store.Common.Role;
using Bugeto_Store.Persistance.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Strore.Application.Interfaces.Contexts;
using System;

namespace EndPoint.Site
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
            services.AddAuthorization(option =>
            {
                option.AddPolicy(UserRoles.Admin, policy => policy.RequireRole(UserRoles.Admin));
                option.AddPolicy(UserRoles.Customer, policy => policy.RequireRole(UserRoles.Customer));
                option.AddPolicy(UserRoles.Operator, policy => policy.RequireRole(UserRoles.Operator));
            }); 

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Authentication/Signin");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });


            services.AddScoped<IDataBaseContext, DataBaseContext>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<IGetRolesService, GetRolesService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IRemoveUserService, RemoveUserService>();
            services.AddScoped<IUserStatusChangeService, UserStatusChangeService>();
            services.AddScoped<IEditUserService, EditUserService>();
            services.AddScoped<IUserLoginService, UserLoginService>();
           


            //facadInject
            services.AddScoped<IProductFacad, ProductFacad>();
           



            //========
            services.AddScoped<IGetMenuItemService, GetMenuItemService>();
            services.AddScoped<IGetCategoryService, GetCategoryService>();
            services.AddScoped<IAddNewSliderService, AddNewSliderService>();
            services.AddScoped<IGetSliderService, GetSliderService>();
            services.AddScoped<IAddHomePageImagesService, AddHomePageImagesService>();
            services.AddScoped<IGetHomePageImageService, GetHomePageImageService>();
            services.AddScoped<ICartService, CartServices>();
            services.AddScoped<IAddRequestPayServices, AddRequestPayService>();

            string connectionString = @"Data Source = DESKTOP-5VBA7GF\GHD; Initial Catalog = StoreDb ; Integrated Security = True; TrustServerCertificate=True;";
            services.AddEntityFrameworkSqlServer().AddDbContext<DataBaseContext>(option=> option.UseSqlServer(connectionString));
            services.AddControllersWithViews();
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

                endpoints.MapControllerRoute(
                      name: "areas",
                       pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
