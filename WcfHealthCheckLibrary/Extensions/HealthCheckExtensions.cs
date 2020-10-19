using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfHealthCheckLibrary.Interfaces;

namespace WcfHealthCheckLibrary.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void AddHealthCheckEndpoint(ServiceHostBase serviceHostBase)
        {
            GenerateHealthEndpoint(serviceHostBase);
        }

        private static void GenerateHealthEndpoint(ServiceHostBase serviceHostBase)
        {
            //HealthCheck için adres tanımını dinamik olarak yapıyoruz. Örnek: http://localhost:12348/ConfigurationService.svc/HealthCheck/
            var address = serviceHostBase.BaseAddresses[0].AbsoluteUri + "/HealthCheck";

            var serviceEndpoint = new ServiceEndpoint(new ContractDescription("IHealthCheckService", "http://tempuri.org/"));

            ContractDescription contractDescription = null;

            var implementedContracts = serviceHostBase
                .GetType()
                .GetProperty("ImplementedContracts", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetField);

            if (implementedContracts != null)
            {
                var contractDic = (Dictionary<string, ContractDescription>)implementedContracts.GetValue(serviceHostBase, null);
                foreach (var contract in contractDic.Values)
                {
                    if (contract.ConfigurationName == typeof(IHealthCheckableService).FullName)
                    {
                        contractDescription = contract;
                        break;
                    }
                }
            }

            if (contractDescription == null) //IHealthCheckService implemente edilmemişse
                return;

            serviceEndpoint.Contract = contractDescription;
            serviceEndpoint.Binding = new WebHttpBinding();
            serviceEndpoint.EndpointBehaviors.Add(new WebHttpBehavior());
            serviceEndpoint.Name = "HealthCheckEndPoint";
            serviceEndpoint.Address = new EndpointAddress(address);

            serviceHostBase.AddServiceEndpoint(serviceEndpoint);
        }
    }
}
