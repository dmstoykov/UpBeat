[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UpBeat.Web.App_Start.DependencyInjectionConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UpBeat.Web.App_Start.DependencyInjectionConfig), "Stop")]

namespace UpBeat.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Microsoft.AspNet.Identity.Owin;

    using Ninject;
    using Ninject.Web.Common;
    using UpBeat.Auth.Contracts;
    using UpBeat.Auth;
    using AutoMapper;
    using UpBeat.Data.Contracts;
    using UpBeat.Data;
    using UpBeat.Services.Contracts;
    using UpBeat.Services;
    using UpBeat.Data.Repositories;
    using UpBeat.Data.UnitOfWork;
    using Ninject.Web.Mvc.FilterBindingSyntax;
    using UpBeat.Web.Infrastructure.Filters;
    using UpBeat.Web.Infrastructure.Attributes;

    public static class DependencyInjectionConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDbContext>().To<MsSqlDbContext>().InRequestScope();
            kernel.Bind<ISaveChanges>().To<SaveChanges>();

            kernel.Bind<IMapper>().ToMethod(ctx => Mapper.Instance).InSingletonScope();
            kernel.Bind<ISignInService>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            kernel.Bind<IUserService>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>());
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<IAlbumService>().To<AlbumService>();
            kernel.Bind<ITrackService>().To<TrackService>();
            kernel.Bind<IArtistService>().To<ArtistService>();
            kernel.Bind<IUsersService>().To<UserService>();

            kernel.BindFilter<SaveChangesFilter>(System.Web.Mvc.FilterScope.Controller, 0).WhenControllerHas<SaveChangesAttribute>();
        }
    }
}
