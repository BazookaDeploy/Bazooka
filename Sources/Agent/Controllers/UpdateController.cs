using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Agent.Controllers
{
    public class UpdateController : ApiController
    {
        [HttpPost]
        public void Update()
        {
            byte[] file = this.Request.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "update.zip"), file);

            System.Diagnostics.ProcessStartInfo procStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = true,
                CreateNoWindow = false,
                WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory,
                FileName = "cmd",
                Arguments = " /c \"Updater.exe\" ",
            };

            System.Diagnostics.Process proc = new System.Diagnostics.Process()
            {
                StartInfo = procStartInfo,
                EnableRaisingEvents = true,
            };

            proc.Start();
            proc.WaitForExit();
        }

    }
}
