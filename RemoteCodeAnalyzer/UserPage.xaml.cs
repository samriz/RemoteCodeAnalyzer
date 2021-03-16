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
using System.Windows.Forms;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class UserPage : Page
    {    
        public UserPage()
        {
            InitializeComponent();
        }
        public UserPage(User user): this()
        {
            FullNameLabel.Content = user.GetFirstName() + " " + user.GetLastName();
        }
        private void progress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void SearchFiles_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog DirectoryExplorer = new FolderBrowserDialog();
            //OpenFileDialog DirectoryExplorer = new OpenFileDialog();
            DirectoryExplorer.ShowDialog();

            //Process.Start("explorer.exe");
        }
    }
}
