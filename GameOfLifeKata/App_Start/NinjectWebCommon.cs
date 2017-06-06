[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectBootstrap.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectBootstrap.App_Start.NinjectWebCommon), "Stop")]

namespace NinjectBootstrap.App_Start
{
    using GameOfLifeDomain;
    using GameOfLifeDomain.Services;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
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
        
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IGameOfLife>().To<GameOfLife>();
            kernel.Bind<ICellNeighborService>().To<CellNeighborService>();
            kernel.Bind<IGenerationEncoderService>().To<GenerationEncoderService>();
        }        
    }
}