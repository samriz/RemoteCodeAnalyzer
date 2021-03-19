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
using System.Windows.Forms; //for FolderBrowserDialog
using System.IO;
using Server;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly Client client;
        FolderBrowserDialog DirectoryExplorer;
        public UserPage()
        {
            InitializeComponent();
            FilesList.IsEnabled = false;
            DirectoryExplorer = new FolderBrowserDialog();
        }
        public UserPage(User user): this()
        {
            FullNameLabel.Content = user.GetFirstName() + " " + user.GetLastName();
            client = new Client("http://localhost:8080/Service");
        }
        private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e){}
        private void SearchFiles_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog DirectoryExplorer = new OpenFileDialog();
            DirectoryExplorer.ShowDialog();
            FolderPathLabel.Content = DirectoryExplorer.SelectedPath;
            
            //Process.Start("explorer.exe");
        }
        private void UploadFiles_Click(object sender, RoutedEventArgs e)
        {
            List<string> files = Directory.GetFiles(DirectoryExplorer.SelectedPath, "*" + ".cs", SearchOption.AllDirectories).ToList();
            FilesList.IsEnabled = true;
            FilesList.ItemsSource = files;
        }
        private void FilesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PickItem();
        }
        private void PickItem()
        {
            //Server.RemoteFileInfo fileInfo = new Server.RemoteFileInfo();

            //fileInfo.FileName = FilesList.SelectedItem.ToString();

            //FileInfo fi = new FileInfo(FilesList.SelectedItem.ToString());

            //fileInfo.Length = fi.Length;


            FileInfo fileInfo = new System.IO.FileInfo(FilesList.SelectedItem.ToString());
            RemoteFileInfo uploadRequestInfo = new RemoteFileInfo();

            using (System.IO.FileStream stream =
                   new System.IO.FileStream(FilesList.SelectedItem.ToString(),
                   System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                uploadRequestInfo.FileName = FilesList.SelectedItem.ToString();
                uploadRequestInfo.Length = fileInfo.Length;
                uploadRequestInfo.FileByteStream = stream;
                client.GetSVC().UploadFile(uploadRequestInfo);
                //clientUpload.UploadFile(stream);
            }

            //System.Windows.MessageBox.Show(FilesList.SelectedItem.ToString());
        }
    }
}