using Microsoft.Owin.Hosting;
using System;
using Topshelf;

namespace Agent
{
    public partial class Service : ServiceControl
    {

        private IDisposable _server = null;

        public Service()
        {
            InitializeComponent();
        }

        public bool Start(HostControl hostControl)
        {
            var baseAddress = System.Configuration.ConfigurationManager.AppSettings["address"];
            _server = WebApp.Start<Startup>(url: baseAddress);
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            if (_server != null)
            {
                _server.Dispose();
            }
            return true;
        }
    }
}
