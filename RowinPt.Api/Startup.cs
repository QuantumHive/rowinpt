using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using AlperAslanApps.AspNetCore.Models;
using Microsoft.AspNetCore.DataProtection;
using AlperAslanApps.AspNetCore.Filters;

namespace RowinPt.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public Startup(
            IHostingEnvironment hostingEnvironment,
            IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                if (_hostingEnvironment.IsProduction())
                {
                    options.Filters.Add(typeof(RequireHttpsAttribute));
                }
                options.Filters.Add(typeof(ValidationExceptionFilter));
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                });

            services.Configure<SendGridMailOptions>(_configuration.GetSection("MailSettings"));
            if (_hostingEnvironment.IsProduction())
            {
                services.Configure<SendGridMailOptions>(options =>
                {
                    options.ApiKey = _configuration["SENDGRID_API_KEY"];
                });
            }

            services.Configure<DataProtectionOptions>(
                options => options.ApplicationDiscriminator = "RowinPt.Api.v1");

            services.IntegrateSimpleInjector(_hostingEnvironment, _configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.ConfigureSimpleInjector();
            Bootstrapper.VerifyInitialization();

            if (_hostingEnvironment.IsDevelopment())
            {
                app.UseCors(policyBuilder => policyBuilder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Location")
                );
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                var origins = _configuration.GetSection(ConfigurationKeys.CorsOrigins)
                    .GetChildren().Select(x => x.Value).ToArray();
                app.UseCors(policyBuilder => policyBuilder
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("Location")
                );

                var rewriteOptions = new RewriteOptions().AddRedirectToHttpsPermanent();
                app.UseRewriter(rewriteOptions);
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=Login}/{id?}");

                routes.MapSpaFallbackRoute(
                        name: "spa-fallback",
                        defaults: new { controller = "Authentication", action = "Login" });
            });
        }
    }
}
