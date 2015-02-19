
namespace Controller
{
    using NServiceBus;
    using NServiceBus.Persistence;
    using NServiceBus.Persistence.Legacy;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.UsePersistence<MsmqPersistence>() ;
            configuration.EndpointName("bazooka.controller");
            configuration.UseSerialization<JsonSerializer>();
            configuration.EnableInstallers();
            configuration.Conventions().DefiningCommandsAs(x => x.GetInterfaces().Contains(typeof(ICommand)));
            

        }
    }
}
