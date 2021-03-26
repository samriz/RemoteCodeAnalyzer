//////////////////////////////////////////////////////////////////////////
// IService.cs - Service interface                                      //
// ver 1.0                                                              //
// Language:    C#, 2020, .Net Framework 4.7.2                          //
// Platform:    MSI GS65 Stealth, Win10                                 //
// Application: CSE681, Project #3&4, Winter 2021                       //
// Author:      Sameer Rizvi, Syracuse University                       //
//              srizvi@syr.edu                                          //
//////////////////////////////////////////////////////////////////////////
/*
 * Package Operations:
 * -------------------
 *  Interface with server.
 */
/* Required Files:
 *   
 *   
 * Maintenance History:
 * --------------------
 * ver 1.2 : 23 February 2021
 * - first release
 */

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
        Task SignInAsync(string email, string password);

        [OperationContract]
        bool IsLoginSuccessful();

        void AuthenticateUser(string email, string password);

        bool UserExists(out string firstName, out string lastName, string email, string password);

        [OperationContract]
        Task UploadFileAsync(string fileName, ConcurrentBag<string> fileText, string userEmail, string projectName);

        [OperationContract, XmlSerializerFormat]
        Task AnalyzeFileAndCreateXML(string fileName, string userEmail, string projectName);

        [OperationContract]
        Task AnalyzeAsync(FileData FD);

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

        //"Get" functions below

        [OperationContract]
        ConcurrentBag<string> GetUsers();

        [OperationContract]
        DirectoryInfo GetUserDirectoryInfo(string userEmail);

        [OperationContract]
        List<string> GetCSharpFilesInUserDirectory(string path, string userEmail, string projectName);

        [OperationContract]
        List<string> GetFileLines(string relativePath);

        [OperationContract]
        User GetUser();

        [OperationContract, XmlSerializerFormat]
        XmlDocument GetAnalysisXML();

        [OperationContract]
        List<string> GetAnalysis();
    }

    [DataContract]
    public struct NewAccountInfo //encapsulates information needed for creating a new account
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
    public class User //for UserPage purposes. creates instance of a user
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
