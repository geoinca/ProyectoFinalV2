using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using INSOLMIMARC.Academy.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
 
using Microsoft.EntityFrameworkCore;

 
 
using Microsoft.AspNetCore.Http;
 


namespace INSOLMIMARC.Academy.Api
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

            services.AddMvcCore()
            .AddAuthorization()
            .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = "http://localhost:52277";
                     options.RequireHttpsMetadata = false;
                     options.ApiName = "APIPublic";
                 });

            services.AddDbContext<SchoolContext>(options => options.UseSqlite("Data Source=SchoolContext.db"));
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseMvc();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello API!");
            });
        }
    }
}
