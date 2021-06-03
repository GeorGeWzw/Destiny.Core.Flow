﻿
using DestinyCore.Dependency;
using DestinyCore.Extensions;
using Destiny.Core.Flow.IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Linq;
using System.Security.Principal;
using DestinyCore.Modules;
using DestinyCore.Events;
using DestinyCore.AutoMapper;
using DestinyCore.Options;

namespace Destiny.Core.Flow.AuthenticationCenter.Startups
{
    [DependsOn(
    typeof(IdentityModule),
    typeof(DependencyAppModule),
    typeof(MediatorAppModule),
    typeof(AuthenticationCenterEntityFrameworkCoreModule),
    typeof(AutoMapperModule),
    typeof(IdentityServer4Module)
    )]
    public class AppWebModule : AppModule
    {
        private string _corePolicyName = string.Empty;
        public override void ApplicationInitialization(ApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
            app.UseRouting();
            if (!_corePolicyName.IsNullOrEmpty())
            {
                app.UseCors(_corePolicyName); //添加跨域中间件
            }
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }

        public override void ConfigureServices(ConfigureServicesContext context)
        {
            var service = context.Services;
#if DEBUG
            service.AddRazorPages().AddRazorRuntimeCompilation();

#else
            service.AddMvc();
#endif

            context.Services.AddTransient<IPrincipal>(provider =>
            {
                IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath; //获取项目路径
            context.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(basePath));
            var configuration = context.GetConfiguration();
            context.Services.Configure<AppOptionSettings>(configuration.GetSection("Destiny"));
            var settings = context.GetConfiguration<AppOptionSettings>("Destiny");
            context.Services.AddObjectAccessor<AppOptionSettings>(settings);
            if (!settings.Cors.PolicyName.IsNullOrEmpty() && !settings.Cors.Url.IsNullOrEmpty()) //添加跨域
            {
                _corePolicyName = settings.Cors.PolicyName;
                context.Services.AddCors(c =>
                {
                    c.AddPolicy(settings.Cors.PolicyName, policy =>
                    {
                        policy.WithOrigins(settings.Cors.Url
                          .Split(",", StringSplitOptions.RemoveEmptyEntries).ToArray())
                        //policy.WithOrigins("http://localhost:5001")//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                        .AllowAnyHeader().AllowAnyMethod().AllowCredentials();//允许cookie;
                    });
                });
            }


        }
    }
}
