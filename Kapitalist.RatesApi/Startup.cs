using Kapitalist.Common;
using Kapitalist.RatesApi.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kapitalist.RatesApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddAutoMapper(expression => expression.AddProfile<AutoMapperProfile>());

            services.AddCommonServices();

            services.AddControllers();

            services.AddGrpc();

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<RatesDataContext>(options =>
                {
                    options.UseNpgsql(Configuration.GetConnectionString("AppDbContext"));
#if DEBUG
                    options.EnableSensitiveDataLogging();
#endif
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Internal Server Error.");
                    });
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<RatesService>();

                endpoints.MapControllers();
            });
        }
    }
}