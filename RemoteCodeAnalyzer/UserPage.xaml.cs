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

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly Client client;
        private readonly FolderBrowserDialog DirectoryExplorer;
        User user;
        public UserPage()
        {
            InitializeComponent();
            InitializeTextBoxes();
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
                if (UsersProjectsTreeView.HasItems)
                {
                    ListDirectory();
                }
            }      
        }
        private string GetFileName(string file)//get just the file name minus the directory
        {
            List<string> pathSubstrings = file.Split('\\').ToList();
            return pathSubstrings[pathSubstrings.Count - 1];
        }     
        private void AnalyzeButton_Click(object sender, RoutedEventArgs e) 
        { 
            PickItem(); 
        }
        private async void PickItem()
        {
            string file = UsersProjectsTreeView.SelectedItem.ToString();
            FileData data = new FileData(file, client.GetSVC().GetFileLines(file, RelativePathBox.Text));
            await client.GetSVC().AnalyzeAsync(data);
            client.GetSVC().SendMessage("I received the analysis results. Thank you.");
            AnalysisResultsGrid.ItemsSource = client.GetSVC().GetAnalysisXML();
        }
        private void InitializeTextBoxes()
        {
            ProjectNameTextBox.Foreground = Brushes.Gray;
            ProjectNameTextBox.FontStyle = FontStyles.Italic;
        }
        private void ProjectNameTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e){ActivateBox(ProjectNameTextBox, "Project Name");}
        private void ActivateBox(System.Windows.Controls.TextBox newlyActiveBox, string text)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
            if (newlyActiveBox.Text == text) newlyActiveBox.Text = "";
        }
        private void UsersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListDirectory();
        }
        public void ListDirectory()
        {
            UsersProjectsTreeView.Items.Clear();
            //var rootDirectoryInfo = new DirectoryInfo(usersDirectory + "\\" + userEmail);
            UsersProjectsTreeView.Items.Add(CreateDirectoryNode(client.GetSVC().GetUserDirectoryInfo(UsersComboBox.SelectedItem.ToString())));
        }
        public TreeViewItem CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            //var directoryNode = new TreeViewItem { Header = directoryInfo.Name };
            var directoryNode = new TreeViewItem();
            directoryNode.Header = directoryInfo.Name;
            foreach (var directory in directoryInfo.GetDirectories())
            {
                directoryNode.Items.Add(CreateDirectoryNode(directory));
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                //directoryNode.Items.Add(new TreeViewItem { Header = file.Name });
                directoryNode.Items.Add(new TreeViewItem().Header = file.Name);
            }
            return directoryNode;
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