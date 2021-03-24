//////////////////////////////////////////////////////////////////////////
// UserPage.xaml.cs - Functionality to allow user to upload a project   //
// to their directory on the server and view all other users' projects  //
// and analyze their files.                                             //
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
 *  View users' directories, choose file to analyze, upload a project
 *  to your own directory, etc.
 */
/* Required Files:
 *   IService.cs, Client.cs
 *   
 * Maintenance History:
 * --------------------
 * ver 1.2 : 23 February 2021
 * - first release
 */

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Forms;//for FolderBrowserDialog
using System.IO;
using Server;
using System.Collections.Concurrent;
using System.Xml;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly Client client; //object that allows us to communicate with service
        private readonly FolderBrowserDialog DirectoryExplorer;
        private User user;
        //bool aFileIsSelected;
        bool anItemInComboBoxIsSelected;
        public UserPage()
        {
            InitializeComponent();
            InitializeTextBoxes();
            //aFileIsSelected = false;
            anItemInComboBoxIsSelected = false;
            DirectoryExplorer = new FolderBrowserDialog();
        }
        public UserPage(User user): this()
        {
            this.user = user;
            FullNameLabel.Content = this.user.GetFirstName() + " " + this.user.GetLastName();
            client = new Client("http://localhost:8080/Service");
            UsersComboBox.ItemsSource = client.GetSVC().GetUsers();
        }
        private void SearchFiles_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog DirectoryExplorer = new OpenFileDialog();
            DirectoryExplorer.ShowDialog();
            FolderPathLabel.Content = DirectoryExplorer.SelectedPath;
            //Process.Start("explorer.exe");
        }

        //upload files to user's directory on service
        private void UploadFiles_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(DirectoryExplorer.SelectedPath)) ErrorMessage.Text = "No valid directory selected.";    
            else
            {
                List<string> files = Directory.GetFiles(DirectoryExplorer.SelectedPath, "*" + "*.cs", SearchOption.AllDirectories).ToList();
                string fileName;
                foreach (var file in files)
                {
                    fileName = GetFileName(file);
                    ConcurrentBag<string> fileLines = new ConcurrentBag<string>(File.ReadAllLines(file).ToList());
                    client.GetSVC().UploadFile(fileName, fileLines, this.user.GetEmail(), ProjectNameTextBox.Text);
                }
                //FilesList.ItemsSource = files;
                if (UsersProjectsTreeView.HasItems) PopulateTreeViewWithDirectory();           
            }      
        }

        //get just the file name from a directory path string
        private string GetFileName(string path)
        {
            List<string> pathSubstrings = new List<string>();
            string fileName = null;
            if (path.Contains("\\"))
            {
                pathSubstrings = path.Split('\\').ToList();
                fileName = pathSubstrings[pathSubstrings.Count - 1];
                return fileName;
            }
            else if (path.Contains("/"))
            {
                pathSubstrings = path.Split('/').ToList();
                fileName = pathSubstrings[pathSubstrings.Count - 1];
                return fileName;
            }
            else 
            {
                //fileName = null;
                return fileName; 
            }
        }

        //choose a file for analysis
        private void AnalyzeButton_Click(object sender, RoutedEventArgs e) 
        {
            ErrorMessage.Text = "";
            //string extension = System.IO.Path.GetExtension(UsersProjectsTreeView.SelectedItem.ToString());
            /*if (UsersProjectsTreeView.SelectedItem.ToString().Contains(".cs"))
            {
                aFileIsSelected = true;
            }*/           
            if (RelativePathTextBox.Text.Length < 1) //make sure user enters something
            {
                ErrorMessage.Text = "Invalid selection(s).";
            }
            else if(RelativePathTextBox.Text == "Enter Relative Path") //make sure user enters something
            {
                ErrorMessage.Text = "Invalid selection(s).";
            }
            /*else if(aFileIsSelected == false)
            {
                ErrorMessage.Text = "Invalid selection(s).";
            }*/
            else if(anItemInComboBoxIsSelected == false)
            {
                ErrorMessage.Text = "Invalid selection(s).";
            }
            else PickItem();
        }
        private void UsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            anItemInComboBoxIsSelected = true;
            PopulateTreeViewWithDirectory();
        }

        //choose a file, send it to service for analysis, and then retrieve the results from the service
        private async void PickItem()
        {
            //string file = UsersProjectsTreeView.SelectedValue.ToString();
            string file = GetFileName(RelativePathTextBox.Text);
            if(file == null) ErrorMessage.Text = "Invalid path.";
            else
            {
                FileData data = new FileData(file, client.GetSVC().GetFileLines(RelativePathTextBox.Text));
                await client.GetSVC().AnalyzeAsync(data);
                client.GetSVC().SendMessage("I received the analysis results. Thank you.");
                List<string> analysisList = client.GetSVC().GetAnalysis();
                AnalysisResults.Items.Clear();
                foreach (string line in analysisList)
                {
                    AnalysisResults.Items.Add(line.ToString());
                }
            }
        }

        //fill the TreeView with the different users' directories and subdirectories
        public void PopulateTreeViewWithDirectory()
        {
            UsersProjectsTreeView.Items.Clear();
            //DirectoryInfo root = new DirectoryInfo(usersDirectory + "\\" + userEmail);
            UsersProjectsTreeView.Items.Add(CreateDirectoryTreeViewItem(client.GetSVC().GetUserDirectoryInfo(UsersComboBox.SelectedItem.ToString())));
        }

        //create an item for the TreeView
        public TreeViewItem CreateDirectoryTreeViewItem(DirectoryInfo directoryInfo)
        {
            TreeViewItem directoryTreeViewItem = new TreeViewItem();
            directoryTreeViewItem.Header = directoryInfo.Name;
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryTreeViewItem.Items.Add(CreateDirectoryTreeViewItem(directory));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                TreeViewItem newItem = new TreeViewItem();
                newItem.Header = file.Name;
                directoryTreeViewItem.Items.Add(newItem);
            }
            return directoryTreeViewItem;
        }
        private void ProjectNameTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActivateBox(ProjectNameTextBox, "Project Name");
        }
        private void RelativePathTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActivateBox(RelativePathTextBox, "Enter Relative Path");
        }
        private void RelativePathTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter)) AnalyzeButton_Click(sender, e);
        }
        private void InitializeTextBoxes()
        {
            ProjectNameTextBox.Foreground = Brushes.Gray;
            ProjectNameTextBox.FontStyle = FontStyles.Italic;
            RelativePathTextBox.Foreground = Brushes.Gray;
            RelativePathTextBox.FontStyle = FontStyles.Italic;
        }
        private void ActivateBox(System.Windows.Controls.TextBox newlyActiveBox, string text)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
            if(newlyActiveBox.Text == text) newlyActiveBox.Text = "";
        }
        /*private void PickItem()
{
FileInfo fileInfo = new FileInfo(FilesList.SelectedItem.ToString());
RemoteFileInfo uploadRequestInfo = new RemoteFileInfo();

using(System.IO.FileStream stream = new System.IO.FileStream(FilesList.SelectedItem.ToString(), System.IO.FileMode.Open, System.IO.FileAccess.Read))
{
uploadRequestInfo.FileName = FilesList.SelectedItem.ToString();
uploadRequestInfo.Length = fileInfo.Length;
uploadRequestInfo.FileByteStream = stream;                
client.GetSVC().UploadFile(uploadRequestInfo);
}
client.GetSVC().Analyze(File.ReadAllLines(FilesList.SelectedItem.ToString()).ToList());
}*/
    }
}