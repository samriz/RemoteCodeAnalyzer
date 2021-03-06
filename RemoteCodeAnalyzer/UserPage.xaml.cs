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
using ListViewItem = System.Windows.Controls.ListViewItem;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly Client client; //object that allows us to communicate with service
        private Microsoft.Win32.OpenFileDialog FileExplorer; //Window to choose file from local computer
        private readonly FolderBrowserDialog DirectoryExplorer; //window to choose directory/folder from local computer
        private readonly User user;
        private bool anItemInComboBoxIsSelected;
        private List<string> uploadedFileNames; //I want just the names of the files without their full paths
        private bool isRelativePathBoxActive;
        private bool isProjectNameBoxActive;
        private bool isProjectNameBox2Active;

        public UserPage()
        {
            InitializeComponent();
            //InitializeTextBoxes();
            anItemInComboBoxIsSelected = false;

            DirectoryExplorer = new FolderBrowserDialog();
            FileExplorer = new Microsoft.Win32.OpenFileDialog();
            FileExplorer.DefaultExt = ".cs"; //default file extension
            FileExplorer.Filter = "C# Files (.cs)|*.cs"; //only show .cs files

            AnalyzeFilesButton.IsEnabled = false;
            AnalyzeSingleFileButton.IsEnabled = false;
            isRelativePathBoxActive = false;
            isProjectNameBoxActive = false;
            isProjectNameBox2Active = false;
        }
        public UserPage(User user): this()
        {
            this.user = user;
            FullNameLabel.Content = this.user.GetFirstName() + " " + this.user.GetLastName() + " (" + this.user.GetEmail() + ")";
            client = new Client("http://localhost:8080/Service");
            UsersComboBox.ItemsSource = client.GetSVC().GetUsers();  
        }

        //for searching for a folder on local computer to upload to server
        private void SearchFiles_Click(object sender, RoutedEventArgs e)
        {
            DirectoryExplorer.ShowDialog();
            FolderPathLabel.Content += DirectoryExplorer.SelectedPath;
            //Process.Start("explorer.exe");
        }

        //for searching for a single file on local computer to upload to server
        private void SearchFile_Click(object sender, RoutedEventArgs e)
        {
            FileExplorer.ShowDialog();
            FolderPathLabel2.Content += FileExplorer.FileName;
        }

        //upload files asynchronously to user's directory on server
        private async void UploadFiles_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage2.Text = "";
            if (!Directory.Exists(DirectoryExplorer.SelectedPath)) 
            { 
                ErrorMessage2.Text = "Invalid directory selected."; 
            }
            else
            {
                List<string> files = Directory.GetFiles(DirectoryExplorer.SelectedPath, "*" + "*.cs", SearchOption.AllDirectories).ToList();
                uploadedFileNames = new List<string>();//will be used for analysis
                foreach(var file in files) 
                {
                    uploadedFileNames.Add(GetFileName(file));
                }
                string fileName;
                foreach (var file in files)
                {
                    fileName = GetFileName(file);
                    ConcurrentBag<string> fileLines = new ConcurrentBag<string>(File.ReadAllLines(file).ToList());
                   await client.GetSVC().UploadFileAsync(fileName, fileLines, this.user.GetEmail(), ProjectNameTextBox.Text);                   
                }
                if (UsersProjectsTreeView.HasItems) PopulateTreeViewWithDirectory();
            }
            AnalyzeFilesButton.IsEnabled = true; //enable analyze button now that we are ready to analyze
            UploadLabel.Content = "Uploading done.";
            UsersProjectsTreeView.Items.Refresh();          
        }

        //Functionality to upload a single file asynchronously to user's directory on server
        private async void UploadSingleFile_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage3.Text = "";
            string file = FileExplorer.FileName;
            if (!File.Exists(file)) ErrorMessage3.Text = "No valid file selected.";           
            else
            {
                string fileName = GetFileName(file);
                ConcurrentBag<string> fileLines = new ConcurrentBag<string>(File.ReadAllLines(file).ToList());
                await client.GetSVC().UploadFileAsync(fileName, fileLines, this.user.GetEmail(), ProjectNameTextBox2.Text);
                if (UsersProjectsTreeView.HasItems) PopulateTreeViewWithDirectory();
            }
            AnalyzeSingleFileButton.IsEnabled = true; //enable analyze button now that we are ready to analyze
            UploadLabel2.Content = "Uploading done.";
            UsersProjectsTreeView.Items.Refresh();
        }

        //choose a file for analysis
        private async void AnalyzeFilesButton_Click(object sender, RoutedEventArgs e) 
        {
            ErrorMessage2.Text = "";

            //go through each file in directory and create their analysis XMLs
            foreach (var fileName in uploadedFileNames)
            {
                await client.GetSVC().AnalyzeFileAndCreateXML(fileName, this.user.GetEmail(), ProjectNameTextBox.Text);
                ErrorMessage2.Text = client.GetSVC().GetMessageFromServer();
                client.GetSVC().SendMessage("I received the analysis results. Thank you.");
            }
            UsersProjectsTreeView.Items.Refresh(); //refresh the view so we can see new files in there
            AnalyzeLabel.Content = "Analyzing done.";
        }
        private async void AnalyzeSingleFileButton_Click(object sender, RoutedEventArgs e)
        {
            string file = FileExplorer.FileName;
            string fileName = GetFileName(file);
            await client.GetSVC().AnalyzeFileAndCreateXML(fileName, this.user.GetEmail(), ProjectNameTextBox2.Text);
            ErrorMessage3.Text = client.GetSVC().GetMessageFromServer();
            client.GetSVC().SendMessage("I received the analysis results. Thank you.");
            AnalyzeLabel2.Content = "Analyzing done.";
            UsersProjectsTreeView.Items.Refresh(); //refresh the view so we can see new files in there       
        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessage1.Text = "";      
            if (RelativePathTextBox.Text.Length < 1) //make sure user enters something
            {
                ErrorMessage1.Text = "Invalid selection(s).";
            }
            else if (RelativePathTextBox.Text == "Enter Relative Path") //make sure user enters something
            {
                ErrorMessage1.Text = "Invalid selection(s).";
            }
            else if (GetExtension(RelativePathTextBox.Text) != "xml")
            {
                if (GetRoot(RelativePathTextBox.Text) != this.user.GetEmail())
                {
                    ErrorMessage1.Text = "You can only view non-XML files in your own directory.";
                }
                else View();
            }
            else if (anItemInComboBoxIsSelected == false)
            {
                ErrorMessage1.Text = "Invalid selection(s).";
            }
            else 
            {
                if (AnalysisResults.Items != null) AnalysisResults.Items.Refresh();
                View(); 
            }
        }

        private async void View()
        {          
            string relativePath = RelativePathTextBox.Text;
            if (relativePath == null || relativePath.Length <= 0) ErrorMessage1.Text = "Invalid path.";
            else
            {
                //XmlDocument analysis = await client.GetSVC().RetrieveFileAndReturnXMLAsync(relativePath);
                List<string> analysis = await client.GetSVC().RetrieveFileAndReturnStringListAsync(relativePath);
                ErrorMessage1.Text = client.GetSVC().GetMessageFromServer();
                client.GetSVC().SendMessage("I received the analysis results. Thank you.");                          
                AnalysisResults.ItemsSource = analysis;
                if (AnalysisResults.Items != null) AnalysisResults.Items.Refresh();
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
            else return fileName;
        }

        //get the extension of the file which would be the last substring after the last dot
        private string GetExtension(string path)
        {
            List<string> pathSubstrings = new List<string>();
            string extension = null;
            if (path.Contains("."))
            {
                pathSubstrings = path.Split('.').ToList();
                extension = pathSubstrings[pathSubstrings.Count - 1];
                return extension;
            }
            else return extension;            
        }

        //get the root of the path string which would be the first substring before the first forward slash
        private string GetRoot(string path)
        {
            List<string> pathSubstrings = new List<string>();
            string root = null;
            if (path.Contains(@"\"))
            {
                pathSubstrings = path.Split('\\').ToList();
                root = pathSubstrings[0];
                return root;
            }
            else return root;
        }

        private void UsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            anItemInComboBoxIsSelected = true;
            PopulateTreeViewWithDirectory();
        }

        //fill the TreeView with the different users' directories and subdirectories
        public void PopulateTreeViewWithDirectory()
        {
            UsersProjectsTreeView.Items.Clear();

            //add the TreeViewItem to the tree of users directories
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
            if(!isProjectNameBoxActive) ActivateBox(ProjectNameTextBox, "Project Name");
            isProjectNameBoxActive = true;
        }
        private void ProjectNameTextBox2_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(!isProjectNameBox2Active) ActivateBox(ProjectNameTextBox2, "Project Name");
            isProjectNameBox2Active = true;
        }
        private void RelativePathTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!isRelativePathBoxActive) ActivateBox(RelativePathTextBox, "Enter Path");
            isRelativePathBoxActive = true;
        }

        //trigger the View button when you press the "Enter" key on the keyboard
        private void RelativePathTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter)) ViewButton_Click(sender, e);
        }
        
        //double click on an item in the tree of directories in order to add it to the RelativePathTextBox
        private void UsersProjectsTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddToRelativePath();
        }
        private void UsersProjectsTreeView_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter)) AddToRelativePath();
        }

        //add the selected TreeViewItem to the RelativePathTextBox
        private void AddToRelativePath()
        {
            if (!isRelativePathBoxActive) ActivateBox(RelativePathTextBox, "Enter Path");
            if (UsersProjectsTreeView.SelectedItem != null)
            {
                string treeItemString = ((TreeViewItem)(UsersProjectsTreeView.SelectedItem)).Header.ToString();
                //don't add a backslash if the last item in the path doesn't have an extension of xml or cs
                if (this.GetExtension(treeItemString) == "xml") RelativePathTextBox.Text += treeItemString;
                else if (this.GetExtension(treeItemString) == "cs") RelativePathTextBox.Text += treeItemString;
                else RelativePathTextBox.Text += treeItemString + @"\";
            }
            else return;
        }

        private void InitializeTextBoxes()
        {
            ProjectNameTextBox.Foreground = Brushes.Gray;
            ProjectNameTextBox.FontStyle = FontStyles.Italic;
            ProjectNameTextBox2.Foreground = Brushes.Gray;
            ProjectNameTextBox2.FontStyle = FontStyles.Italic;
            RelativePathTextBox.Foreground = Brushes.Gray;
            RelativePathTextBox.FontStyle = FontStyles.Italic;
        }
        private void ActivateBox(System.Windows.Controls.TextBox newlyActiveBox, string text)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
            if (newlyActiveBox.Text == text) newlyActiveBox.Clear();
        }
    }
}