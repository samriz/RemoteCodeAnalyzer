//////////////////////////////////////////////////////////////////////////
// NewAccountPage.xaml.cs - Functionality to allow a user to create a   //
// new account.                                                         //
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
 *  Enter is a first name, last name, email, and password. Email addresses
 *  linked to existing account will be denied.
 */
/* Required Files:
 *   IService.cs, Client.cs
 *   
 * Maintenance History:
 * --------------------
 * ver 1.2 : 23 February 2021
 * - first release
 */

using Server;
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
using System.Collections.Concurrent;

namespace RemoteCodeAnalyzer
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccountPage : Page
    {
        //private readonly string xmlFileName;
        private readonly Client client; //object that allows us to communicate with service
        public NewAccountPage()
        {
            InitializeComponent();
            InitializeTextBoxes();
            //xmlFileName = @"../../Users.xml";
            client = new Client("http://localhost:8080/Service");
        }
        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {
            //client.GetSVC().AddNewUser(FirstNameTextBox.Text, LastNameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text);
            NewAccountInfo newAccountInfo = new NewAccountInfo(FirstNameTextBox.Text, LastNameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text);
            //await client.GetSVC().AddNewAccountAsync(newAccountInfo);
            //if (client.GetSVC().WasUserAdded()) 
            if(await client.GetSVC().AddNewAccountAsync(newAccountInfo))
            {
                MessageBox.Show(client.GetSVC().GetMessageFromServer() + " You may now login with your new account. Redirecting to Login page.");
                GoToLoginSignupPage();
            }
            else ErrorMessage.Text = client.GetSVC().GetMessageFromServer();
        }
        private void GoToLoginSignupPage()
        {
            LoginSignupPage login = new LoginSignupPage();
            this.NavigationService.Navigate(login);
        }
        
        private void InitializeTextBoxes()
        {
            FirstNameTextBox.Foreground = Brushes.Gray;
            LastNameTextBox.Foreground = Brushes.Gray;
            EmailTextBox.Foreground = Brushes.Gray;
            PasswordTextBox.Foreground = Brushes.Gray;

            EmailTextBox.FontStyle = FontStyles.Italic;
            PasswordTextBox.FontStyle = FontStyles.Italic;
            FirstNameTextBox.FontStyle = FontStyles.Italic;
            LastNameTextBox.FontStyle = FontStyles.Italic;
        }
        private void ActivateBox(TextBox newlyActiveBox, string text)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
            if (newlyActiveBox.Text == text) newlyActiveBox.Text = "";
        }
        private void InactivateBox(TextBox newlyInactiveBox) => newlyInactiveBox.Background = Brushes.White;
        private void FirstNameTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActivateBox(FirstNameTextBox, "First Name");
            InactivateBox(LastNameTextBox);
            InactivateBox(EmailTextBox);
            InactivateBox(PasswordTextBox);
            /*LastNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            FirstNameTextBox.Background = Brushes.AliceBlue;
            FirstNameTextBox.Text = "";*/
        }
        private void LastNameTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InactivateBox(FirstNameTextBox);
            ActivateBox(LastNameTextBox, "Last Name");
            InactivateBox(EmailTextBox);
            InactivateBox(PasswordTextBox);
            /*FirstNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.AliceBlue;
            LastNameTextBox.Text = "";*/
        }
        private void EmailTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InactivateBox(FirstNameTextBox);
            InactivateBox(LastNameTextBox);
            ActivateBox(EmailTextBox, "Email");
            InactivateBox(PasswordTextBox);
            /*FirstNameTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.AliceBlue;
            EmailTextBox.Text = "";*/
        }
        private void PasswordTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            InactivateBox(FirstNameTextBox);
            InactivateBox(LastNameTextBox);
            InactivateBox(EmailTextBox);
            ActivateBox(PasswordTextBox, "Password");
            /*FirstNameTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;            
            PasswordTextBox.Background = Brushes.AliceBlue;
            PasswordTextBox.Text = "";*/
        }

        private void FirstNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab))
            {
                InactivateBox(FirstNameTextBox);
                ActivateBox(LastNameTextBox, "Last Name");
                InactivateBox(EmailTextBox);
                InactivateBox(PasswordTextBox);
            }
        }

        private void LastNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab))
            {
                InactivateBox(FirstNameTextBox);
                InactivateBox(LastNameTextBox);
                ActivateBox(EmailTextBox, "Email");
                InactivateBox(PasswordTextBox);
            }
        }

        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab))
            {
                InactivateBox(FirstNameTextBox);
                InactivateBox(LastNameTextBox);
                InactivateBox(EmailTextBox);
                ActivateBox(PasswordTextBox, "Password");
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                if(FirstNameTextBox.Text.Length < 1)
                {
                    ErrorMessage.Text = "Incomplete first name.";
                }
                else if(LastNameTextBox.Text.Length < 1)
                {
                    ErrorMessage.Text = "Incomplete last name.";
                }
                else if(EmailTextBox.Text.Length < 1)
                {
                    ErrorMessage.Text = "Incomplete email address.";
                }
                else if(PasswordTextBox.Text.Length < 1)
                {
                    ErrorMessage.Text = "Incomplete password.";
                }
                else
                {
                    SignUp_Click(sender, e);
                }
            }
        }
        /*private void SignUp_Click(object sender, RoutedEventArgs e)
{
AddNewUser(FirstNameTextBox.Text, LastNameTextBox.Text, EmailTextBox.Text, PasswordTextBox.Text);
LoginSignupPage login = new LoginSignupPage();
MessageBox.Show("Your new account was successfully created. You may now login with your new account.");
this.NavigationService.Navigate(login);
}*/
        /*public void AddNewUser(string FirstName, string LastName, string email, string password)
        {
            XmlDocument UsersXML = new XmlDocument();
            //UsersXML.Load(@"C:\Users\srizv\OneDrive - Syracuse University\Syracuse University\Courses\CSE 681 (2)\Project 3\RemoteCodeAnalyzer\RemoteCodeAnalyzer\Users.xml");
            UsersXML.Load(xmlFileName);

            XmlElement userElem = UsersXML.CreateElement("User");
            userElem.SetAttribute("FirstName", FirstName);
            userElem.SetAttribute("LastName", LastName);
            
            XmlElement loginElem = UsersXML.CreateElement("Login");
            loginElem.SetAttribute("Email", email);
            loginElem.SetAttribute("Password", password);
            
            userElem.AppendChild(loginElem);
            UsersXML.DocumentElement.AppendChild(userElem);
            //UsersXML.Save(Console.Out);
            UsersXML.Save(@"../../Users.xml");
        }*/
    }
}