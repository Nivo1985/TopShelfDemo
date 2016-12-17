using Topshelf;

namespace TopShelfDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(serviceConfig =>
            {
                serviceConfig.UseNLog();

                serviceConfig.Service<ServiceLogic>(serviceInstance =>
                {
                    serviceInstance.ConstructUsing(() => new ServiceLogic());
                    serviceInstance.WhenStarted(serviceLogic => serviceLogic.Start());
                    serviceInstance.WhenStopped(serviceLogic => serviceLogic.Stop());
                });

                serviceConfig.EnableServiceRecovery(option =>
                {
                    option.RestartService(5);
                    option.RestartService(30);
                    option.RestartComputer(60,"Error with top shelf demo");
                });

                serviceConfig.SetServiceName("TopShelfDemoServiceName");
                serviceConfig.SetDisplayName("Top Shelf Demo Service Display Name");

                serviceConfig.StartAutomatically();
            });
        }
    }
}

