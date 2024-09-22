using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Base64ConverterSolution.Web.SignalRHub;

namespace ContainerBase64
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
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    var allowedOrigins = Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                    builder.WithOrigins(allowedOrigins!)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetIsOriginAllowed((host) => true);
                });
            });

            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<EncodingHub>("/signalr/encodinghub").RequireCors("AllowSpecificOrigins");
            });

        }
    }
}
