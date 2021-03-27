//////////////////////////////////////////////////////////////////////////
// Service.cs - Implement service interface                             //
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
 *  Implements functions declared in service interface.
 */
/* Required Files:
 *   
 *   
 * Maintenance History:
 * --------------------
 * ver 1.2 : 23 February 2021
 * - first release
 */

using CodeAnalyzer;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        private readonly string usersDirectory;
        private User user;
        private string serverMessage;
        private string clientMessage;
        private FunctionTracker FuncTrac;
        private AnalysisDisplayer AD;
        //private XmlDocument analysisXML;
        //private bool wasUserAdded;       
        private List<string> analysisLines;

        public BasicService()
        {
            usersXML = @"../../Users.xml";
            serverMessage = "Default message";
            clientMessage = "";
            usersDirectory = @"../../Users";
        }

        //upload a file into a project folder
        public async Task UploadFileAsync(string fileName, ConcurrentBag<string> fileText, string userEmail, string projectName)
        {
            Task<bool> uploadTask = Task.Run(() =>
            {
                //if the project folder doesn't exist, a new one will be created
                string directoryPath = CreateNewProjectFolder(userEmail, projectName) + "/";
                string filePath = directoryPath + fileName;
                File.WriteAllLines(filePath, fileText);
                if (File.Exists(filePath)) return true;
                else return false;
            });
            if (await uploadTask == false) serverMessage = fileName + " was not uploaded.";
            else serverMessage = fileName + " uploaded!";
            Console.WriteLine(serverMessage);
        }

        public async Task AnalyzeFileAndCreateXML(string fileName, string userEmail, string projectName)
        {
            Task<bool> analysisTask = Task.Run(() =>
            {
                string path = "..\\..\\Users" + "\\" + userEmail + "\\" + projectName + "\\" + fileName;
                List<string> fileLines = GetFileLines(path);
                FuncTrac = new FunctionTracker(fileLines);
                AD = new AnalysisDisplayer(fileName, null, FuncTrac.GetFunctionNodes());
                if (AD.GetFunctionNodes() == null || AD.GetFunctionNodes().Count <= 0) 
                {
                    return false;                 
                }
                else
                {
                    string xmlPath = path + "_analysis" + "(" + DateTime.Now.ToString("yyyy-MM-dd(HH-mm-ss)") + ")" + ".xml";
                    AD.GetAnalysisInXML().Save(xmlPath);
                    if (File.Exists(xmlPath)) return true;                    
                    else return false;
                }
            });
            if (await analysisTask) serverMessage = "Task " + analysisTask.Id + " has finished creating the analysis xml for " + fileName;
            else 
            {
                string message1 = fileName + " cannot be analyzed. It may not contain any classes and/or functions.";
                Console.WriteLine("Task " + analysisTask.Id + " did not create the analysis xml for " + fileName);
                serverMessage = message1;
            }
            Console.WriteLine(serverMessage);
        }

        //retrieve file on server asynchronously and return List<string>
        public async Task<List<string>> RetrieveFileAndReturnStringListAsync(string relativePath)
        {
            string path = "..\\..\\Users" + "\\" + relativePath;
            List<string> lines = new List<string>();

            if (File.Exists(path))
            {
                lines = GetFileLines(path);
                //return true;
            }

            Task<bool> retriever = Task.Run(() =>
            {
                if (File.Exists(path)) 
                {
                    lines = GetFileLines(path);
                    return true;
                }
                else
                {
                    serverMessage = "File does not exist at that path. It may be an invalid path.";
                    return false;
                }
            });
            if (await retriever) return lines;
            else return null;
        }

        //retrieve file on server asynchronously and return XmlDocument
        public async Task<XmlDocument> RetrieveFileAndReturnXMLAsync(string relativePath)
        {
            string path = "..\\..\\Users" + "\\" + relativePath;
            XmlDocument xmlFile = new XmlDocument();
            Task<bool> retriever = Task.Run(() =>
            {
                if (File.Exists(path))
                {
                    xmlFile.Load(path);
                    return true;
                }
                else
                {
                    serverMessage = "File does not exist at that path. It may be an invalid path.";
                    return false;
                }
            });
            if (await retriever) return xmlFile;
            else return null;
        }

        //analyze a file asynchronously
        public async Task AnalyzeAsync(FileData FD)
        {
            //Console.WriteLine(Encoding.ASCII.GetString(fileBuffer));
            Task<List<string>> analysisTask = Task.Run(() =>
            {
                FuncTrac = new FunctionTracker(FD.fileLines);
                AD = new AnalysisDisplayer(FD.fileName, null, FuncTrac.GetFunctionNodes());
                return AD.GetAnalysis();
            });
            analysisLines = await analysisTask;
            serverMessage = "Task " + analysisTask.Id + " has finished executing. Results returned.";
            Console.WriteLine(serverMessage);
        }

        //login asynchronously
        public async Task<bool> LoginAsync(string email, string password)
        {
            bool loginSuccessful = false;
            Task loginTask = Task.Run(() =>
            {
                if (email.Length > 0 && password.Length > 0)
                {
                    AuthenticateUser(email, password);
                    if (this.user == null)
                    {                        
                        serverMessage = "Incorrect Login.";
                        //Console.WriteLine(serverMessage);
                        loginSuccessful = false;
                    }
                    else
                    {                       
                        serverMessage = "Login successful! Returned page!";
                        loginSuccessful = true;
                    }
                }
                else
                {
                    serverMessage = "Email or Password fields cannot be empty.";
                    loginSuccessful = false;
                }
            });
            await loginTask;
            Console.WriteLine(serverMessage);
            return loginSuccessful;
        }

        //instantiate user for UserPage
        public void AuthenticateUser(string email, string password)
        {         
            if (UserExists(out string firstName, out string lastName, email, password))
            {
                this.user = new User(firstName, lastName, email, password);
                Console.WriteLine("User authenticated.");
            }
            else 
            { 
               this.user = null;
               Console.WriteLine("User not authenticated.");
            }
        }

        //tells us whether the user exists in the server or not. if it does, then retrieve their first and last names
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

        //makes sure we don't have duplicate email addresses in the system
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

        //create a new entry in Users.xml asynchronously
        public async Task<bool> AddNewAccountAsync(NewAccountInfo newAccountInfo)
        {
            bool wasUserAdded = false;
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
                return wasUserAdded;
            }
            else
            {
                serverMessage = "An account associated with this email address already exists.";
                Console.WriteLine(serverMessage);
                return wasUserAdded;
            }
        }

        //create a new entry in Users.xml asynchronously
        /*public async Task AddNewAccountAsync(NewAccountInfo newAccountInfo)
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
        }*/

        //create a new account folder when a new account is created
        public string CreateNewAccountFolder(string folderName)
        {
            string path = usersDirectory + "/" + folderName;
            Directory.CreateDirectory(path);
            return usersDirectory + "/" + folderName;
        }

        //allows the user to upload a new project to their directory
        public string CreateNewProjectFolder(string userEmail, string projectName)
        {
            string path = usersDirectory + "/" + userEmail + "/" + projectName;

            //if project directory doesn't exist, then create that directory and return its path
            if (!Directory.Exists(usersDirectory + "\\" + userEmail + "\\" + projectName)) 
            { 
                Directory.CreateDirectory(path);
                return usersDirectory + "/" + userEmail + "/" + projectName;
            }
            else //if the project directory does exist, then return its path
            {
                return usersDirectory + "\\" + userEmail + "\\" + projectName;
            }           
        }       

        //get all the users in Users.xml
        public ConcurrentBag<string> GetUsers()
        {
            ConcurrentBag<string> users = new ConcurrentBag<string>();
            XmlDocument UsersXML = new XmlDocument();
            UsersXML.Load(usersXML);
            XmlNodeList elemList = UsersXML.GetElementsByTagName("Login");
            for (int i = 0; i < elemList.Count; i++){users.Add(elemList[i].Attributes.GetNamedItem("Email").Value);}
            return users;
        }

        //get all file text in a given file on the server
        public List<string> GetFileLines(string path)
        {
            //string path = usersDirectory + "/" + relativePath;
            return File.ReadAllLines(path).ToList<string>();
        }
        public DirectoryInfo GetUserDirectoryInfo(string userEmail)
        {
            return new DirectoryInfo(usersDirectory + "\\" + userEmail);
        }
        public List<string> GetCSharpFilesInUserDirectory(string path, string userEmail, string projectName)
        {
            List<string> files = Directory.GetFiles(path, "*" + "*.cs", SearchOption.AllDirectories).ToList();
            return files;
        }
        public void SendMessage(string message)
        {
            //clientMessage = message;
            Console.WriteLine("Message received by service: {0}", clientMessage = message);
        }

        //Getters Below
        public string GetMessageFromServer() => serverMessage;
        public List<string> GetAnalysis() => analysisLines;
        //public bool WasUserAdded() => wasUserAdded;
        public User GetUser() => user;
    }
}