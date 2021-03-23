using CodeAnalyzer;
using System;
using System.Collections.Generic;
using System.IO;
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
    //implement functions defined in IService.cs
    //what server will do when request comes from client

    [ServiceBehavior(InstanceContextMode =InstanceContextMode.PerSession)]

    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BasicService : IBasicService
    {
        private readonly string usersXML;
        //FileStream targetStream;
        User user;
        private string serverMessage;
        private string clientMessage;
        private byte[] fileBuffer;
        private FunctionTracker FuncTrac;
        private AnalysisDisplayer AD;
        private XmlDocument analysisXML;
        private bool loginSuccessful;
        private bool wasUserAdded;
        private string usersDirectory;

        public BasicService()
        {
            //targetStream = null;
            usersXML = @"../../Users.xml";
            serverMessage = "Default message";
            clientMessage = "";
            usersDirectory = @"../../Users";
        }
        public XmlDocument AnalyzeFile(FileData FT)
        {
            //Console.WriteLine(Encoding.ASCII.GetString(fileBuffer));
            FuncTrac = new FunctionTracker(FT.fileLines);
            AD = new AnalysisDisplayer(FT.fileName, null, FuncTrac.GetFunctionNodes());    
            serverMessage = "Analysis done. Results returned.";
            Console.WriteLine(serverMessage);
            analysisXML = AD.GetAnalysisInXML();
            return analysisXML;
        }
        public async Task AnalyzeAsync(FileData FT)
        {
            //Console.WriteLine(Encoding.ASCII.GetString(fileBuffer));
            Task<XmlDocument> analysisTask = Task.Run(() =>
            {
                FuncTrac = new FunctionTracker(FT.fileLines);
                AD = new AnalysisDisplayer(FT.fileName, null, FuncTrac.GetFunctionNodes());
                return AD.GetAnalysisInXML();
            });
            analysisXML = await analysisTask;
            serverMessage = "Task " + analysisTask.Id + " has finished executing. Results returned.";
            Console.WriteLine(serverMessage);           
        }
        public bool Login(string email, string password)
        {
            if (email.Length > 0 && password.Length > 0) 
            { 
                AuthenticateUser(email, password); 
                if(user == null)
                {                   
                    serverMessage = "Incorrect Login.";
                    Console.WriteLine(serverMessage);
                    return false;
                }
                else
                {                    
                    serverMessage = "Login successful! Returned page!";
                    Console.WriteLine(serverMessage);
                    return true;
                }
            }
            else
            {
                serverMessage = "Email or Password fields cannot be empty.";
                Console.WriteLine(serverMessage);
                return false;
            }
        }
        public async Task SignInAsync(string email, string password)
        {
            Task<bool> loginTask = Task.Run(() =>
            {
                if (email.Length > 0 && password.Length > 0)
                {
                    AuthenticateUser(email, password);
                    if (user == null)
                    {                        
                        serverMessage = "Incorrect Login.";
                        //Console.WriteLine(serverMessage);
                        return false;
                    }
                    else
                    {                       
                        serverMessage = "Login successful! Returned page!";
                        //Console.WriteLine(serverMessage);
                        return true;
                    }
                }
                else
                {
                    serverMessage = "Email or Password fields cannot be empty.";
                    //Console.WriteLine(serverMessage);
                    return false;
                }
            });
            loginSuccessful = await loginTask;
            Console.WriteLine(serverMessage);
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
            UsersXML.Load(usersXML);

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
        public void AddNewUser(NewAccountInfo newAccountInfo)
        {
            XmlDocument UsersXML = new XmlDocument();
            //UsersXML.Load(@"C:\Users\srizv\OneDrive - Syracuse University\Syracuse University\Courses\CSE 681 (2)\Project 3\RemoteCodeAnalyzer\RemoteCodeAnalyzer\Users.xml");
            UsersXML.Load(usersXML);

            XmlElement userElem = UsersXML.CreateElement("User");
            userElem.SetAttribute("FirstName", newAccountInfo.firstName);
            userElem.SetAttribute("LastName", newAccountInfo.lastName);

            XmlElement loginElem = UsersXML.CreateElement("Login");

            //need to validate that this email doesn't already have an account associated with it
            if (!AnAccountWithThisEmailAlreadyExists(newAccountInfo.email))
            {
                loginElem.SetAttribute("Email", newAccountInfo.email);
                loginElem.SetAttribute("Password", newAccountInfo.password);

                userElem.AppendChild(loginElem);
                UsersXML.DocumentElement.AppendChild(userElem);
                //UsersXML.Save(Console.Out);
                UsersXML.Save(usersXML);
                serverMessage = "New account " + newAccountInfo.email + " created.";
                Console.WriteLine(serverMessage);
            }
            else
            {
                serverMessage = "An account associated with this email address already exists.";
                return;
            }        
        }
        public bool AnAccountWithThisEmailAlreadyExists(string email)
        {
            XmlDocument UsersXML = new XmlDocument();
            UsersXML.Load(usersXML);

            XmlNodeList elemList = UsersXML.GetElementsByTagName("Login");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (elemList[i].Attributes.GetNamedItem("Email").Value == email) return true;
                
            }
            return false;
        }
        public async Task AddNewAccountAsync(NewAccountInfo newAccountInfo)
        {
            Task<bool> addUserTask = Task.Run(() =>
            {
                XmlDocument UsersXML = new XmlDocument();
                UsersXML.Load(usersXML);

                XmlElement userElem = UsersXML.CreateElement("User");
                userElem.SetAttribute("FirstName", newAccountInfo.firstName);
                userElem.SetAttribute("LastName", newAccountInfo.lastName);

                XmlElement loginElem = UsersXML.CreateElement("Login");

                //need to validate that this email doesn't already have an account associated with it
                if (!AnAccountWithThisEmailAlreadyExists(newAccountInfo.email))
                {
                    loginElem.SetAttribute("Email", newAccountInfo.email);
                    loginElem.SetAttribute("Password", newAccountInfo.password);
                    userElem.AppendChild(loginElem);
                    UsersXML.DocumentElement.AppendChild(userElem);
                    UsersXML.Save(usersXML);
                    CreateNewAccountFolder(newAccountInfo.email);
                    return true;
                }
                else return false;
            });
            wasUserAdded = await addUserTask;
            if (wasUserAdded)
            {
                serverMessage = "New account " + newAccountInfo.email + " successfully created.";
                Console.WriteLine(serverMessage);
            }
            else
            {
                serverMessage = "An account associated with this email address already exists.";
                Console.WriteLine(serverMessage);
            }
        }
        public string CreateNewAccountFolder(string folderName)
        {
            string path = usersDirectory + "/" + folderName;
            Directory.CreateDirectory(path);
            return usersDirectory + "/" + folderName;
        }
        public string CreateNewProjectFolder(string userEmail, string projectName)
        {
            string path = usersDirectory + "/" + userEmail + "/" + projectName;
            Directory.CreateDirectory(path);
            return usersDirectory + "/" + userEmail + "/" + projectName;
        }
        public void UploadFile(string fileName, List<string> fileText, string userEmail, string projectName)
        {   
            string directoryPath = CreateNewProjectFolder(userEmail, projectName) + "/";
            string filePath = directoryPath + fileName;
            //File.Create(filePath);
            File.WriteAllLines(filePath, fileText);
        }
        public void SendMessage(string message)
        {
            clientMessage = message;
            Console.WriteLine("Message received by service: {0}", clientMessage);
            //print message that client had sent
            //Console.WriteLine("Message received by service: {0}", message);
        }
        //public string GetMessage() => "New message from Service.";
        public string GetMessageFromServer() => serverMessage;
        public User GetUser() => user;
        public XmlDocument GetAnalysisXML() => analysisXML;
        public bool IsLoginSuccessful() => loginSuccessful;
        public bool WasUserAdded() => wasUserAdded;
        /*public void UploadFile(RemoteFileInfo request)
        {
            FileStream targetStream = null;
            Stream sourceStream = request.FileByteStream;
            //string uploadFolder = @".";
            //string filePath = Path.Combine(uploadFolder, request.FileName);
            string filePath = request.FileName;

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 65000 byte chunks
                const int bufferLen = 65000;
                fileBuffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(fileBuffer, 0, bufferLen)) > 0)
                {
                    // save to output stream
                    targetStream.Write(fileBuffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }
        }*/
    }
}