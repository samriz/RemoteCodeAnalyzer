using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
            Func<string> fnc = () =>
            {
                svc.SendMessage(message);
                return "Service succeeded";
            };
            ServiceRetryWrapper(fnc);
        }
        //make sure service is up and running
        string ServiceRetryWrapper(Func<string> fnc)
        {
            int count = 0;
            string message;
            while (true)
            {
                try
                {
                    message = fnc.Invoke();
                    break;
                }
                catch(Exception exc)
                {
                    if(count > 4)
                    {
                        return "Max retries exceeded";
                    }
                    Console.WriteLine(exc.Message);
                    Console.WriteLine("Service failed {0} times - trying again.", ++count);
                    Thread.Sleep(100);
                    continue;
                }
            }
            return message;
        }
        static void Main(string[] args)
        {

        }
    }
}
