using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; //need this for WCF
using System.Xml;
using System.Windows;
using System.Runtime.Serialization;
using System.Collections.Concurrent;
using System.Windows.Controls;
using System.IO;

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

        [OperationContract]
        Task SignInAsync(string email, string password);

        [OperationContract]
        bool IsLoginSuccessful();

        void AuthenticateUser(string email, string password);

        bool UserExists(out string firstName, out string lastName, string email, string password);
        
        //[OperationContract]
        //void UploadFile(RemoteFileInfo request);

        [OperationContract]
        User GetUser();

        //[OperationContract, XmlSerializerFormat]
        //XmlDocument AnalyzeFile(FileData FD);

        [OperationContract]
        List<string> AnalyzeFile(FileData FD);

        [OperationContract]
        Task AnalyzeAsync(FileData FD);

        //[OperationContract, XmlSerializerFormat]
        //XmlDocument GetAnalysisXML();
        [OperationContract]
        List<string> GetAnalysis();

        [OperationContract]
        void AddNewUser(NewAccountInfo newAccountInfo);

        [OperationContract]
        Task AddNewAccountAsync(NewAccountInfo newAccountInfo);

        bool AnAccountWithThisEmailAlreadyExists(string email);

        [OperationContract]
        bool WasUserAdded();

        string CreateNewAccountFolder(string folderName);

        [OperationContract]
        string CreateNewProjectFolder(string userEmail, string projectName);

        [OperationContract]
        void UploadFile(string fileName, ConcurrentBag<string> fileText, string userEmail, string projectName);

        [OperationContract]
        ConcurrentBag<string> GetUsers();

        [OperationContract]
        DirectoryInfo GetUserDirectoryInfo(string userEmail);

        [OperationContract]
        List<string> GetFileLines(string relativePath);
    }

    [DataContract]
    public struct NewAccountInfo
    {
        [DataMember]
        public string firstName, lastName, email, password;
        public NewAccountInfo(string FirstName, string LastName, string email, string password)
        {
            this.firstName = FirstName;
            this.lastName = LastName;
            this.email = email;
            this.password = password;
        }
    }

    [DataContract]
    //[MessageContract]
    public class FileData //contains name of the file and it's extracted text
    {
        //[MessageHeader(MustUnderstand = true)]
        [DataMember]
        public string fileName;

        //[MessageBodyMember(Order = 1)]
        [DataMember]
        public List<string> fileLines;
        //public ConcurrentBag<string> fileLines;

        public FileData() { }
        public FileData(string fileName, List<string> fileLines)
        {
            this.fileName = fileName;
            this.fileLines = fileLines;
        }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        private string firstName, lastName, email, password;

        public User() { }
        public User(string email, string password)
        {
            this.firstName = "";
            this.lastName = "";
            this.email = email;
            this.password = password;
        }
        public User(string firstName, string lastName, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }
        public string GetFirstName() => firstName;
        public string GetLastName() => lastName;
        public string GetEmail() => email;
        public string GetPassword() => password;
        public void SetFirstName(string firstName) => this.firstName = firstName;
        public void SetLastName(string lastName) => this.lastName = lastName;
        public void SetEmail(string email) => this.email = email;
        public void SetPassword(string password) => this.password = password;
    }
    /*[MessageContract]
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
    }*/
}
