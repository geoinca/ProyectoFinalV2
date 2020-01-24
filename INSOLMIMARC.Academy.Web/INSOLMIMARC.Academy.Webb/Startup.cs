using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using INSOLMIMARC.Academy.Webb.Models;
using Polly;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;

namespace INSOLMIMARC.Academy.Webb
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<INSOLMIMARCAcademyWebbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("INSOLMIMARCAcademyWebbContext")));

            ///authv1 001 
            ////  services.AddAuthentication(options => { options.DefaultAuthenticateScheme = "Bearer"; })
            /*
                                    .AddIdentityServerAuthentication(options =>
                                     {
                                         // base-address of your identityserver
                                         options.Authority = "http://localhost:52277/connect/token";
                                         // name of the API resource
                                         options.ApiName = "INSOLMIMARC";
                                         options.ApiSecret = "INSOLMIMARC";
                                         options.EnableCaching = true;
                                         options.CacheDuration = TimeSpan.FromMinutes(1000); // that's the default
                                         options.SupportedTokens = SupportedTokens.Both;
                                     });
            */

            services.AddAuthentication( options =>
            {
                options.DefaultScheme = "APIPublic";
                options.DefaultChallengeScheme = "oidc";
            })

            .AddOpenIdConnect("oidc", options =>
            {
                options.SignInScheme = "Cookies";

                options.Authority = "http://localhost:52277/connect/token";
                options.RequireHttpsMetadata = false;

                options.ClientId = "INSOLMIMARC";
                options.ClientSecret = "INSOLMIMARC";
                options.ResponseType = "id_token";
                
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.Scope.Add("APIPublic");
               
                options.ClaimActions.MapJsonKey("website", "website");
            });


            ////



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            ///authv1 0001
            app.UseAuthentication();
            ///
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
