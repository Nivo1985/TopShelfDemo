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

                serviceConfig.SetServiceName("TopShelfDemoServiceName");
                serviceConfig.SetDisplayName("Top Shelf Demo Service Display Name");

                serviceConfig.StartAutomatically();
            });
        }
    }
}

