﻿using CodeAnalyzer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
  /*
  * InstanceContextMode determines the activation policy, e.g.:
  * 
  *   PerCall    - remote object created for each call
  *              - runs on thread dedicated to calling client
  *              - this is default activation policy
  *   PerSession - remote object created in session on first call
  *              - session times out unless called again within timeout period
  *              - runs on thread dedicated to calling client
  *   Singleton  - remote object created in session on first call
  *              - session times out unless called again within timeout period
  *              - runs on one thread so all clients see same instance
  *              - access must be synchronized
  */
    //implement functions defined in Server.cs
    //what server will do when request comes from client

    [ServiceBehavior(InstanceContextMode =InstanceContextMode.PerSession)]

    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BasicService : IBasicService
    {
        /*static void Main(string[] args)
        {
        }*/
        private readonly string usersData;
        //string errorMessage;
        User user;
        private string serverMessage;
        private string clientMessage;

        public BasicService()
        {
            usersData = @"../../Users.xml";
            serverMessage = "Default message";
            clientMessage = "";
        }

        public User GetUser() => user;

        public void Analyze(string file)
        {
            DirectorySearcher DS = new DirectorySearcher(file);
        }
        public void UploadFile(RemoteFileInfo request)
        {
            //spawn thread or task for analysis
            Task AnalysisTask = Task.Run(() =>
            {

            });
        }
        //public bool Login(string email, string password, out string infoMessage)
        public bool Login(string email, string password)
        {
            if (email.Length > 0 && password.Length > 0) 
            { 
                AuthenticateUser(email, password); 
                if(user == null)
                {
                    //infoMessage = "Incorrect Login.";
                    //Console.WriteLine(infoMessage);
                    serverMessage = "Incorrect Login.";
                    Console.WriteLine(serverMessage);
                    return false;
                }
                else
                {
                    //infoMessage = "Login successful! Returned page!";
                    //Console.WriteLine(infoMessage);
                    serverMessage = "Login successful! Returned page!";
                    Console.WriteLine(serverMessage);
                    return true;
                }
            }
            else
            {
                //infoMessage = "Email or Password fields cannot be empty.";
                //Console.WriteLine(infoMessage);
                serverMessage = "Email or Password fields cannot be empty.";
                Console.WriteLine(serverMessage);
                return false;
            }
        }
        public void AuthenticateUser(string email, string password)
        {
            //string firstName = "";
            //string lastName = "";
            //user = new User(email, password);

            if (UserExists(out string firstName, out string lastName, email, password))
            {
                user = new User(firstName, lastName, email, password);
                //this.NavigationService.Navigate(userpage);
                //return user;
            }
            else 
            {
                user = null;
                //return null;//MessageBox.Show("Incorrect Login.");
            }
        }
        public bool UserExists(out string firstName, out string lastName, string email, string password)
        {
            XmlDocument UsersXML = new XmlDocument();
            UsersXML.Load(usersData);

            XmlNodeList elemList = UsersXML.GetElementsByTagName("Login");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (elemList[i].Attributes.GetNamedItem("Email").Value == email && elemList[i].Attributes.GetNamedItem("Password").Value == password)
                {
                    firstName = elemList[i].ParentNode.Attributes.GetNamedItem("FirstName").Value;
                    lastName = elemList[i].ParentNode.Attributes.GetNamedItem("LastName").Value;
                    return true;
                }
            }
            firstName = "";
            lastName = "";
            return false;
        }
        public void SendMessage(string message)
        {
            clientMessage = message;
            Console.WriteLine("Message received by service: {0}", clientMessage);
            //print message that client had sent
            //Console.WriteLine("Message received by service: {0}", message);
        }
        //public void SendMessage(XmlDocument xmlMessage)
        //{
        //    //print message that client had sent
        //    throw new NotImplementedException();
        //}
        //public string GetMessage() => "New message from Service.";
        public string GetMessageFromServer() => serverMessage;
    }
}