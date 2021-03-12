using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;

namespace WCF
{
    [ServiceContract]
    public interface IBasicService
    {
        //clients have to use this interface to communicate with server
        [OperationContract]
        void SendMessage(string message);
        
        [OperationContract]
        void SendMessage(XmlDocument xmlMessage);

        [OperationContract]
        string GetMessage();
        
        [OperationContract] //exposed to client 
        void Login();
        
        [OperationContract]
        void UploadFiles();
    }

    [MessageContract]
    //public class RemoteFileInfo : IDisposable
    //{

    //}
    class Server
    {
    }
}
