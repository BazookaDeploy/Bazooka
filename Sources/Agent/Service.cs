using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace Agent
{
    public partial class Service : ServiceBase
    {

        private IDisposable _server = null;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var baseAddress = System.Configuration.ConfigurationManager.AppSettings["address"];
            _server = WebApp.Start<Startup>(url: baseAddress);
        }

        protected override void OnStop()
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            base.OnStop();
        }
    }
}
