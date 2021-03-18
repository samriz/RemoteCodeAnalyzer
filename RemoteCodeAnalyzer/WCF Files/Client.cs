using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server;

namespace RemoteCodeAnalyzer
{
    class Client/* : System.ServiceModel.ClientBase<IBasicService>, IBasicService*/
    {
        private IBasicService svc;//we're going to use this object to communicate with the service i.e. use it to call the functions declared in the interface. basically like we're invoking methods on the server
        public Client(string url)
        {
            WSHttpBinding binding = new WSHttpBinding();
            EndpointAddress address = new EndpointAddress(url);

            //A factory that creates channels of different types that are used by clients to send messages to variously configured service endpoints.
            ChannelFactory<IBasicService> factory = new ChannelFactory<IBasicService>(binding, address);
            svc = factory.CreateChannel();
        }

        public IBasicService GetSVC() => svc;

        public void SendMessage(string message)
        {
            Func<string> fnc = () =>
            {
                svc.SendMessage(message);
                return "Service succeeded";
            };
            ServiceRetryWrapper(fnc); //make sure service is up and running
        }
        public string GetMessage()
        {
            string message;
            Func<string> fnc = () =>
            {
                message = svc.GetMessage();
                return message;
            };
            return ServiceRetryWrapper(fnc);
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
        /*static void Main(string[] args)
        {
            Console.Title = "BasicHttp Client";
            Console.WriteLine("\n Starting Programmatic Basic Service Client");
            Console.WriteLine("===============================");
            string message = args[0];
            string url = "http://localhost:8080/BasicService";
            Client client = new Client(url);

            client.SendMessage(message);
            client.SendMessage(message);
            client.SendMessage(message);
            client.SendMessage(message);
        }*/
    }
}
