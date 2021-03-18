using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
    //implement functions defined in Server.cs
    //what server will do when request comes from client

    [ServiceBehavior(InstanceContextMode =InstanceContextMode.PerSession)]

    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BasicService : IBasicService
    {
        /*static void Main(string[] args)
        {
        }*/
        string errorMessage;
        public void Login(string email, string password)
        {
            if (email.Length > 0 && password.Length > 0) AuthenticateUser(email, password);
            else
            {
                errorMessage = "Email or Password fields cannot be empty.";
            }
        }
        void AuthenticateUser(string email, string password)
        {
            throw new NotImplementedException();
        }
        public void SendMessage(string message)
        {
            //print message that client had sent
            Console.WriteLine("Message received by service: {0}", message);
        }
        //public void SendMessage(XmlDocument xmlMessage)
        //{
        //    //print message that client had sent
        //    throw new NotImplementedException();
        //}
        public string GetMessage()
        {
            return "New message from Service.";
        }
        public void UploadFile(RemoteFileInfo request)
        {
            throw new NotImplementedException();
        }

        
    }
}