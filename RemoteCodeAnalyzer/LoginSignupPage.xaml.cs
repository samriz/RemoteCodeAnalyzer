//////////////////////////////////////////////////////////////////////////
// LoginSignupPage.xaml.cs - Login page for existing accounts.          //
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
 *  Log into user account. If one wishes to create a new account, this page
 *  can take them to the New Account Page.
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
    /// Interaction logic for LoginOrCreateAccount.xaml
    /// </summary>
    public partial class LoginSignupPage : Page
    {
        private User user;
        //string xmlFileName = Environment.CurrentDirectory + @"\Users.xml";
        //private readonly string usersData;
        private readonly Client client; //object that allows us to communicate with service

        public LoginSignupPage()
        {
            InitializeComponent();
            //InitializeTextBoxes();
            //user = new User();
            client = new Client("http://localhost:8080/Service");
            //usersData = @"../../Users.xml";
        }
        
        //event handler below
        private void LoginButton_Click(object sender, RoutedEventArgs e){Login();}
        
        //functionality for logging into user page
        private async void Login()
        {
            //string message;
            UserPage userpage;
            //if (client.GetSVC().Login(EmailTextBox.Text, PasswordTextBox.Password))
            //if(client.GetSVC().IsLoginSuccessful())
            if(await client.GetSVC().LoginAsync(EmailTextBox.Text, PasswordTextBox.Password))
            {
                //user = new User(client.GetSVC().GetUser().GetFirstName(), client.GetSVC().GetUser().GetLastName(), client.GetSVC().GetUser().GetEmail(), client.GetSVC().GetUser().GetPassword());
                user = client.GetSVC().GetUser();
                userpage = new UserPage(user);
                this.NavigationService.Navigate(userpage);
            }
            //else ErrorLabel.Text = message;
            else ErrorMessage.Text = client.GetSVC().GetMessageFromServer();
        }

        //go to NewAccountPage so user can create a new account if they wish
        private void NewAccountButton_Click(object sender, RoutedEventArgs e)
        {
            NewAccountPage NA = new NewAccountPage();
            this.NavigationService.Navigate(NA);
        }
        private void EmailTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActivateBox(EmailTextBox, "Email");
            InactivateBox(PasswordTextBox);
        }
        private void PasswordTextBox_ActivateOnClick(object sender, DependencyPropertyChangedEventArgs e)
        {
            ActivateBox(PasswordTextBox);
            InactivateBox(EmailTextBox);
        }
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (EmailTextBox.Text.Length == 0) EmailTextBox.Background = Brushes.White;
            if (PasswordTextBox.Password.Length == 0) PasswordTextBox.Background = Brushes.White;
        }
        private void EmailTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab))
            {
                ActivateBox(PasswordTextBox);
                InactivateBox(EmailTextBox);
            }
        }
        private void ActivateBox(TextBox newlyActiveBox, string text)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
            if (newlyActiveBox.Text == text) newlyActiveBox.Text = "";
        }
        private void ActivateBox(PasswordBox newlyActiveBox)
        {
            newlyActiveBox.Foreground = Brushes.Black;
            newlyActiveBox.FontStyle = FontStyles.Normal;
            newlyActiveBox.Background = Brushes.AliceBlue;
        }
        private void InactivateBox(TextBox newlyInactiveBox) => newlyInactiveBox.Background = Brushes.White;
        private void InactivateBox(PasswordBox newlyInactiveBox) => newlyInactiveBox.Background = Brushes.White;
        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter)) Login();
        }
        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab)) ActivateBox(EmailTextBox, "Email");
        }
        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab)) ActivateBox(EmailTextBox, "Email");          
        }
        private void InitializeTextBoxes()
        {
            EmailTextBox.Foreground = Brushes.Gray;
            PasswordTextBox.Foreground = Brushes.Gray;
            EmailTextBox.FontStyle = FontStyles.Italic;
            PasswordTextBox.FontStyle = FontStyles.Italic;
        }
        /*
   public delegate void RoutedEventHandler(object sender, RoutedEventArgs e);
   public event RoutedEventHandler Click;

   Click += Login_Click //we are registering Login_Click method to the Click event
   Click?.Invoke(sender,e); //invoke handler(s)
   */
        //sender is button
    }
}