using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; //need this for WCF
using System.Xml;
using System.Windows;
using System.Runtime.Serialization;

namespace Server
{
    [ServiceContract]
    public interface IBasicService
    {
        //clients have to use this interface to communicate with server
        //OperationContract = exposed to client 
        [OperationContract]
        void SendMessage(string message);

        [OperationContract]
        string GetMessageFromServer();

        [OperationContract]
        bool Login(string email, string password);

        void AuthenticateUser(string email, string password);

        bool UserExists(out string firstName, out string lastName, string email, string password);
        
        [OperationContract]
        void UploadFile(RemoteFileInfo request);

        [OperationContract]
        User GetUser();

        [OperationContract, XmlSerializerFormat]
        XmlDocument Analyze(FileText FT);
        //XmlDocument Analyze(string fileName, List<string> fileLines);

        //[OperationContract, XmlSerializerFormat]
        //XmlDocument GetAnalysis();
    }

    [DataContract]
    //[MessageContract]
    public class FileText
    {
        //[MessageHeader(MustUnderstand = true)]
        [DataMember]
        public string fileName;

        //[MessageBodyMember(Order = 1)]
        [DataMember]
        public List<string> fileLines;

        public FileText() { }
        public FileText(string fileName, List<string> fileLines)
        {
            this.fileName = fileName;
            this.fileLines = fileLines;
        }
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
