using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Web.Http.SelfHost;
using twainNative;
using windowsService.utilityClasses;


namespace windowsService
{

    public partial class AlQemamScanningService : ServiceBase
    {
        public AlQemamScanningService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                twainHandler.Handler = this.ServiceHandle;
                twainHandler.setTwain();

                var config = new HttpSelfHostConfiguration("http://localhost:8080");
                config.EnableCors();
                config.Routes.MapHttpRoute(
                    name: "Default",
                    routeTemplate: "{controller}/{action}/{id}",
                    defaults: new { controller = "Home", action = "checkService", id = RouteParameter.Optional }
                    );
                config.MaxBufferSize = (20 * 1024 * 1024);
                config.MaxReceivedMessageSize = (20 * 1024 * 1024);
                HttpSelfHostServer server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();

            }
            catch (Exception)
            {
                throw;
            }

        }

        protected override void OnStop()
        {

        }
    }
}
