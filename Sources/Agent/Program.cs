using Topshelf;

namespace Agent
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var host = HostFactory.New(x =>                               
            {
                x.Service<Service>();
                x.ApplyCommandLine();
                x.SetDescription("Bazooka agent");
                x.SetDisplayName("AgentSmith");
                x.StartAutomaticallyDelayed();
            });
            host.Run();
        }
    }
}
