using FluentValidation;
using FluentValidation.AspNetCore;
using Kapitalist.Common;
using Kapitalist.Common.ApiModels;
using Kapitalist.Common.Extensions;
using Kapitalist.RatesApi.Database;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddControllers()
                .AddFluentValidation();

            services.AddTransient<IValidator<RatesSnapshot>, RatesSnapshotValidator>();

            services.AddGrpc(options => { options.EnableDetailedErrors = true; });

            services.AddDbContext<RatesDataContext>(options =>
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
                app.UseProdExceptionHandler();
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