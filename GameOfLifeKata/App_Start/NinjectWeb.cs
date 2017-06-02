[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Qualtrax.Web.App_Start.NinjectWeb), "Start")]

namespace Qualtrax.Web.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject.Web.Common;

    public static class NinjectWeb
    {
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}