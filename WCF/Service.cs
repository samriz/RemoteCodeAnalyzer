using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WCF
{
    //implement functions defined in Server.cs
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
            throw new NotImplementedException();
        }

        public void sendMessage(XmlDocument xmlMessage)
        {
            throw new NotImplementedException();
        }

        public void UploadFiles()
        {
            throw new NotImplementedException();
        }
    }
}
