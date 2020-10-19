using System.ServiceModel;
using System.ServiceModel.Web;

namespace WcfHealthCheckLibrary.Interfaces
{
    [ServiceContract]
    public interface IHealthCheckableService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "",
            BodyStyle = WebMessageBodyStyle.Bare,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string HealthCheck();
    }
}
