using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF;

namespace RemoteCodeAnalyzer
{
    class Client
    {
        IBasicService svc;
        Client(string url)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            EndpointAddress address = new EndpointAddress(url);

            //A factory that creates channels of different types that are used by clients to send messages to variously configured service endpoints.
            ChannelFactory<IBasicService> factory = new ChannelFactory<IBasicService>(binding, address);
            svc = factory.CreateChannel();
        }

        void SendMessage(string message)
        {

        }
        static void Main(string[] args)
        {

        }
    }
}
