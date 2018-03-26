using AlperAslanApps.Core;
using AlperAslanApps.Core.Decorators;
using AlperAslanApps.Core.EntityFrameworkCore.Decorators;
using AlperAslanApps.Core.EntityFrameworkCore.Services;
using AlperAslanApps.Core.Services;
using AlperAslanApps.Core.SimpleInjector.Services;
using AlperAslanApps.Core.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RowinPt.DataAccess;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RowinPt.Business;
using Microsoft.ApplicationInsights;
using AlperAslanApps.AspNetCore.Services;
using RowinPt.Business.Services;
using RowinPt.Domain;
using AlperAslanApps.AspNetCore.Models;
using Microsoft.AspNetCore.DataProtection;
using AlperAslanApps.AspNetCore;
using RowinPt.Api.Services;

namespace RowinPt.Api
{
    public static class Bootstrapper
    {
        private static readonly Container Container = new Container();

        public static void VerifyInitialization() => Container.Verify();

        public static void IntegrateSimpleInjector(this IServiceCollection services, IHostingEnvironment environment, IConfiguration configuration)
        {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);

            services.InitializeSimpleInjector(environment, configuration);
        }

        public static void InitializeSimpleInjector(this IServiceCollection services, IHostingEnvironment environment, IConfiguration configuration)
        {
            var assemblies = AssemblyLoader.SearchAssemblies("rowinpt").ToArray();

            Container.RegisterSingleton(() => configuration);
            Container.RegisterSingleton(() => environment);
            Container.RegisterSingleton<ApplicationSettings>();

            Container.RegisterServices(environment);
            Container.RegisterQueryHandlers(assemblies);
            Container.RegisterCommandHandlers(assemblies);
            Container.RegisterValidators(assemblies);
            Container.RegisterDataServices(configuration);
        }

        public static void RegisterServices(this Container container, IHostingEnvironment environment)
        {
            container.RegisterSingleton(() => new TelemetryClient());
            container.RegisterSingleton<IScopeStarter, SimpleInjectorAsyncScopeStarter>();
            container.RegisterSingleton<IHttpContextAccessor, HttpContextAccessor>();
            container.RegisterSingleton<ITimeProvider, SystemClockProvider>();
            container.RegisterSingleton<IUserContext, ClaimsUserContext>();
            container.RegisterSingleton<IAuthenticator, Authenticator>();
            container.RegisterSingleton<IPasswordHasher, IdentityPasswordHasherAdapter>();
            container.RegisterSingleton(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            container.RegisterSingleton<IEnvironment, HostingEnvironmentAdapter>();
            container.RegisterSingleton<ICompanyContext>(() => new CompanyContext(CompanyIds.RowinPt));
            container.RegisterSingleton<ITokenProvider<UserModel>, UserTokenProvider>();
            container.RegisterSingleton<IHost, WebHost>();
            container.RegisterSingleton<ISessionManager, SessionManager>();
            container.Register<ITokenGenerator, TokenGenerator>();
            container.Register<IEditInfoHandler, EditInfoTracker>(Lifestyle.Scoped);

            container.Register<IDataProtector>(() =>
            {
                var provider = container.GetInstance<IDataProtectionProvider>();
                return provider.CreateProtector("RowinPt.Api.DataProtectorTokenProvider.v1");
            });

            if (environment.IsDevelopment())
            {
                container.RegisterSingleton<IEmailService, TelemetryMailService>();
            }
            else
            {
                container.RegisterSingleton<IEmailService, SendGridMailService>();
            }
        }

        public static void RegisterQueryHandlers(this Container container, IEnumerable<Assembly> assemblies)
        {
            container.RegisterSingleton<IQueryProcessor, QueryProcessor>();

            container.Register(typeof(IQueryHandler<,>), assemblies);

            var decorators = new[]
            {
                typeof(EnsureLinqEvaluationQueryHandlerDecorator<,>),
                typeof(SetUserContextQueryHandlerDecorator<,>)
            };

            foreach (var decorator in decorators)
            {
                container.RegisterDecorator(typeof(IQueryHandler<,>), decorator);
            }

            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LifetimeScopeQueryHandlerProxy<,>), Lifestyle.Singleton);
        }

        public static void RegisterCommandHandlers(this Container container, IEnumerable<Assembly> assemblies)
        {
            container.Register(typeof(ICommandHandler<>), assemblies);

            var decorators = new[]
            {
                typeof(SaveChangesCommandHandlerDecorator<>),
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(SetUserContextCommandHandlerDecorator<>)
            };

            foreach (var decorator in decorators)
            {
                container.RegisterDecorator(typeof(ICommandHandler<>), decorator);
            }

            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(LifetimeScopeCommandHandlerProxy<>), Lifestyle.Singleton);
            container.RegisterDecorator(typeof(ICommandHandler<>), typeof(ObjectValidationCommandHandlerDecorator<>), Lifestyle.Singleton);
        }

        public static void RegisterValidators(this Container container, IEnumerable<Assembly> assemblies)
        {
            container.RegisterCollection(typeof(IValidator<>), assemblies);
            container.RegisterSingleton(typeof(IValidator<>), typeof(CompositeValidator<>));
        }

        public static void RegisterDataServices(this Container container, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConfigurationKeys.MainDatabase);
            var dbContextRegistration = Lifestyle.Scoped.CreateRegistration(() => new RowinPtContext(connectionString), container);
            container.AddRegistration<RowinPtContext>(dbContextRegistration);
            container.AddRegistration<DbContext>(dbContextRegistration);

            Type RepositoryTypeFactory(TypeFactoryContext typeContext)
            {
                var entityType = typeContext.ServiceType.GetGenericArguments()[0];
                return typeof(Repository<,>).MakeGenericType(typeof(RowinPtContext), entityType);
            }

            container.RegisterConditional(typeof(IReader<>), RepositoryTypeFactory, Lifestyle.Transient, predicate => true);
            container.RegisterConditional(typeof(IRepository<>), RepositoryTypeFactory, Lifestyle.Transient, predicate => true);
        }

        public static void ConfigureSimpleInjector(this IApplicationBuilder app)
        {
            Container.RegisterMvcControllers(app);
            Container.CrossWire<ILoggerFactory>(app);
            Container.CrossWire<IOptions<PasswordHasherOptions>>(app);
            Container.CrossWire<IOptions<SendGridMailOptions>>(app);
            Container.CrossWire<IDataProtectionProvider>(app);
        }
    }
}
