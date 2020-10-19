using System;
using WcfHealthCheckLibrary.Behaviors;
using WcfHealthCheckLibrary.Interfaces;

namespace WcfHealthCheck
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MyService.svc or MyService.svc.cs at the Solution Explorer and start debugging.
    [HealthCheckBehavior]
    public class MyService : IMyService, IHealthCheckableService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string HealthCheck()
        {
            //Your Healthcheck business.
            return "LIVE";
        }
    }
}
