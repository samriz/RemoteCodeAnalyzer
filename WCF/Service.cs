using CodeAnalyzer;
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
        private readonly string usersData;
        //string errorMessage;
        User user;

        public BasicService()
        {
            usersData = @"../../Users.xml";
        }

        public User GetUser() => user;

        public void Analyze(string directoryPath)
        {
            DirectorySearcher DS = new DirectorySearcher(directoryPath);
        }

        public bool Login(string email, string password, out string infoMessage)
        {
            if (email.Length > 0 && password.Length > 0) 
            { 
                AuthenticateUser(email, password); 
                if(user == null)
                {
                    infoMessage = "Incorrect Login.";
                    Console.WriteLine(infoMessage);
                    return false;
                }
                else
                {
                    infoMessage = "Login successful! Returned page!";
                    Console.WriteLine(infoMessage);
                    return true;
                }
            }
            else
            {
                infoMessage = "Email or Password fields cannot be empty.";
                Console.WriteLine(infoMessage);
                return false;
            }
        }
        public void AuthenticateUser(string email, string password)
        {
            string firstName = "";
            string lastName = "";
            //user = new User(email, password);

            if (UserExists(out firstName, out lastName, email, password))
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