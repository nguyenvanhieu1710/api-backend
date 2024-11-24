using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens; 
using Microsoft.OpenApi.Models; 
using System.Text; 

namespace API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) 
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) 
                .AddEnvironmentVariables(); 

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            // config Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Admin", Version = "v1" }); 

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // config JWT
            var appSettingsSection = Configuration.GetSection("AppSettings"); 
            services.Configure<AppSettings>(appSettingsSection); 

            var appSettings = appSettingsSection.Get<AppSettings>(); 
            var key = Encoding.ASCII.GetBytes(appSettings.Secret); 

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true; 
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true, 
                    IssuerSigningKey = new SymmetricSecurityKey(key), 
                    ValidateIssuer = false, 
                    ValidateAudience = false 
                };
            });

            services.AddTransient<IDatabaseHelper, DatabaseHelper>(); 
            services.AddTransient<IVoucherBLL, VoucherBLL>(); 
            services.AddTransient<IVoucherDAL, VoucherDAL>(); 
            services.AddTransient<ICategoryBLL, CategoryBLL>();
            services.AddTransient<ICategoryDAL, CategoryDAL>();
            services.AddTransient<IProductBLL, ProductBLL>();
            services.AddTransient<IProductDAL, ProductDAL>();
            services.AddTransient<IUsersBLL, UsersBLL>();
            services.AddTransient<IUsersDAL, UsersDAL>();
            services.AddTransient<IStaffBLL, StaffBLL>();
            services.AddTransient<IStaffDAL, StaffDAL>();
            services.AddTransient<IAccountDAL, AccountDAL>();
            services.AddTransient<IAccountBLL, AccountBLL>();
            services.AddTransient<IOrdersBLL, OrdersBLL>();
            services.AddTransient<IOrderDAL, OrderDAL>();
            services.AddTransient<IImportBillBLL, ImportBillBLL>();
            services.AddTransient<IImportBillDAL, ImportBillDAL>();
            services.AddTransient<ISupplierBLL, SupplierBLL>();
            services.AddTransient<ISupplierDAL, SupplierDAL>();
            services.AddTransient<IAdvertisementBLL, AdvertisementBLL>();
            services.AddTransient<IAdvertisementDAL, AdvertisementDAL>();
            services.AddTransient<INewsBLL, NewsBLL>();
            services.AddTransient<INewsDAL, NewsDAL>();            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication(); 
            app.UseAuthorization(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            });
        }
    }
}
