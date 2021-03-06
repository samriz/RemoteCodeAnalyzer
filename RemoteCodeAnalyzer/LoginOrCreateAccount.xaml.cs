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
using System.Xml;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for LoginOrCreateAccount.xaml
    /// </summary>
    public partial class LoginOrCreateAccount : Page
    {
        User user;
        public LoginOrCreateAccount()
        {
            InitializeComponent();
            InitializeTextBoxes();
        }
        private void InitializeTextBoxes()
        {
            EmailTextBox.Foreground = Brushes.Gray;
            PasswordTextBox.Foreground = Brushes.Gray;
            EmailTextBox.FontStyle = FontStyles.Italic;
            PasswordTextBox.FontStyle = FontStyles.Italic;
            //EmailTextBox.IsEnabled = false;
            //PasswordTextBox.IsEnabled = false;
        }

        private bool AuthenticateUser()
        {
            //if the user's email address is in Users.xml and if matches the password that corresponds with it, return true;
            return false;
        }
        
        private void Login_Click(object sender, RoutedEventArgs e) //event handler
        {
            /*
            public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
            public event RoutedEventHandler Click;
             
            Click += Login_Click //we are registering Login_Click method to the Click event
            Click?.Invoke(sender,e); //invoke handler(s)
            */
            //sender is button
            user = new User(this.EmailTextBox.Text, this.PasswordTextBox.Text);
            UserPage userpage = new UserPage(user);

            //string xmlFileName = Environment.CurrentDirectory + @"\Users.xml";
            string xmlFileName = @"C:\Users\srizv\OneDrive - Syracuse University\Syracuse University\Courses\CSE 681 (2)\Project 3\RemoteCodeAnalyzer\RemoteCodeAnalyzer\Users.xml";

            if (UserExists(xmlFileName, user.GetEmail(), user.GetPassword()))
            {
                this.NavigationService.Navigate(userpage);
            }
            else MessageBox.Show("User doesn't exist. Please create a new account");
        }

        private bool UserExists(string xmlfilename, string email, string password)
        {
            XmlDocument UsersXML = new XmlDocument();
            UsersXML.Load(xmlfilename);

            XmlNodeList elemList = UsersXML.GetElementsByTagName("Login");
            for (int i = 0; i < elemList.Count; i++)
            {
                if (elemList[i].Attributes.GetNamedItem("Email").Value == email && elemList[i].Attributes.GetNamedItem("Password").Value == password)
                //if (elemList[i].OuterXml.Contains(email) && elemList[i].OuterXml.Contains(password))
                {
                    return true;
                }
            }
            return false;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            NewAccount NA = new NewAccount();
            this.NavigationService.Navigate(NA);
        }

        private void EmailTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            EmailTextBox.Foreground = Brushes.Black;
            EmailTextBox.FontStyle = FontStyles.Normal;

            PasswordTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.AliceBlue;
            if(EmailTextBox.Text == "Email") EmailTextBox.Text = "";
        }

        private void PasswordTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            PasswordTextBox.Foreground = Brushes.Black;
            PasswordTextBox.FontStyle = FontStyles.Normal;

            EmailTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.AliceBlue;
            if (PasswordTextBox.Text == "Password") PasswordTextBox.Text = "";
        }

        private void DockPanel_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(EmailTextBox.Text.Length == 0)
            {
                EmailTextBox.Background = Brushes.White;
            }
            if (PasswordTextBox.Text.Length == 0)
            {
                PasswordTextBox.Background = Brushes.White;
            }
        }
    }
}