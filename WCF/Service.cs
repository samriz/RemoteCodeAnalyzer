using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WCF
{
    //implement functions defined in Server.cs
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class Service : IBasicService
    {
        /*static void Main(string[] args)
        {
        }*/
        public void Login()
        {
            throw new NotImplementedException();
        }

        public void sendMessage(string message)
        {
            Console.WriteLine("Message received by service: {0}", message);
        }

        public void sendMessage(XmlDocument xmlMessage)
        {
            throw new NotImplementedException();
        }

        public string getMessage()
        {
            return "New message from Service.";
        }

        public void UploadFiles()
        {
            throw new NotImplementedException();
        }
    }
}
