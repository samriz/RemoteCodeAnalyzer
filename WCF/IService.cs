﻿using System;
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
        UserPage Login(string email, string password, out string infoMessage);

        UserPage AuthenticateUser(string email, string password);

        bool UserExists(ref string firstName, ref string lastName, string email, string password);
        
        [OperationContract]
        void UploadFile(RemoteFileInfo request);

        [OperationContract]
        string GetErrorMessage();
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
