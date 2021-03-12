using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF
{
    class Host
    {
        //define endpoints and open them for the connections
        static ServiceHost CreateChannel(string url)
        {
            //"url" is the url we are going to listen on
            BasicHttpBinding binding = new BasicHttpBinding(); //describes how messages are going to be encoded and how they are going to be transfered over the network
            Uri address = new Uri(url);
            Type service = typeof(Service);
            ServiceHost host = new ServiceHost(service, address);
            host.AddServiceEndpoint(typeof(IBasicService), binding, address); //exposes client to IBasicService class
            return host;
        }
        static void Main(string[] args)
        {
            Console.Title = "BasicHttp Service Host";
            Console.WriteLine("Starting programmatic basic service");
            ServiceHost host = null;
            try
            {
                host = CreateChannel("http://localhost:8080/Service"); //opening up connection on port 8080 for clients
                host.Open(); //after this, server can accept requests (starts listening)
                Console.WriteLine("Started Service - Press key to exit.");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            host.Close();
        }
    }
}
