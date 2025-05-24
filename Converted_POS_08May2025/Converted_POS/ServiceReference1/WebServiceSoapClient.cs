using System.ServiceModel;

namespace ServiceReference1
{
    internal class WebServiceSoapClient
    {
        private BasicHttpBinding binding;
        private EndpointAddress endpoint;

        public WebServiceSoapClient(BasicHttpBinding binding, EndpointAddress endpoint)
        {
            this.binding = binding;
            this.endpoint = endpoint;
        }
    }
}