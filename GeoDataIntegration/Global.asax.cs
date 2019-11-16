using System.Web.Http;
using GeoDataIntegration.Interfaces;
using GeoDataIntegration.Services;
using SimpleInjector;

namespace GeoDataIntegration
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Register<IGeoDataFileManager, GeoDataFileManager>(Lifestyle.Singleton);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
