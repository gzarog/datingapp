
using datingapp.api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using datingapp.api.Helpers;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using datingapp.api.Models;

namespace datingapp.api
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
           IdentityBuilder builder =services.AddIdentityCore<User>(opt => {
               	opt.Password.RequireDigit= false;
                opt.Password.RequiredLength= 4;
                opt.Password.RequireUppercase= false;
                opt.Password.RequireNonAlphanumeric= false;

           });
           
           builder = new IdentityBuilder( builder.UserType , typeof(Role), builder.Services);
           builder.AddEntityFrameworkStores<DataContext>();
           builder.AddRoleValidator<RoleValidator<Role>>();
           builder.AddRoleManager<RoleManager<Role>>();
           builder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey=true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer= false,
                        ValidateAudience=false
                    };
                });


            services.AddControllers().AddNewtonsoftJson(opt => {
                 opt.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             });;
            services.AddDbContext<DataContext>(x =>{
                    x.UseLazyLoadingProxies();
                    x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
           
             services.AddMvc(options =>{
                  
                  var policy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();

                  options.Filters.Add(new AuthorizeFilter(policy));
                  options.EnableEndpointRouting = false;
             })
             .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
             .AddNewtonsoftJson(opt => {
                 opt.SerializerSettings.ReferenceLoopHandling= Newtonsoft.Json.ReferenceLoopHandling.Ignore;
             });
           
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddAutoMapper(typeof(DatingRepository).Assembly);
            
            services.AddScoped<IDatingRepository,DatingRepository>();
            
            services.AddScoped<LogUserActivity>();
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

                app.UseExceptionHandler(builder => {
                    builder.Run(async context =>{
                        context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error !=null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
           
            //app.UseHttpsRedirection();
           

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseEndpoints(endPoints => {
                endPoints.MapControllers();
                //endPoints.MapFallbackToController("Index","Fallback");

            });
           
        }
    }
}
