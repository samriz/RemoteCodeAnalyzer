using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Xml;
using System.Windows;

namespace Server
{
    [ServiceContract]
    public interface IBasicService
    {
        //clients have to use this interface to communicate with server
        [OperationContract]
        void SendMessage(string message);
        
        //[OperationContract]
        //void SendMessage(XmlDocument xmlMessage);

        [OperationContract]
        string GetMessage();
        
        [OperationContract] //exposed to client 
        bool Login(string email, string password, out string infoMessage);

        void AuthenticateUser(string email, string password);

        bool UserExists(out string firstName, out string lastName, string email, string password);
        
        [OperationContract]
        void UploadFile(RemoteFileInfo request);

        [OperationContract]
        User GetUser();

        //[OperationContract]
        //string GetErrorMessage();

        void Analyze(string directoryPath);
    }

    [MessageContract]
    //send an object of this for messages
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if(FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}
