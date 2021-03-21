﻿using Server;
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
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccountPage : Page
    {
        //private readonly string xmlFileName;
        Client client;
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
            await client.GetSVC().AddUserAsync(newAccountInfo);
            GoToLoginSignupPage();
        }
        private void GoToLoginSignupPage()
        {
            LoginSignupPage login = new LoginSignupPage();
            MessageBox.Show("Your new account was successfully created. You may now login with your new account. Click Ok to continue to Login page.");
            this.NavigationService.Navigate(login);
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
            //EmailTextBox.IsEnabled = false;
            //PasswordTextBox.IsEnabled = false;
        }

        private void FirstNameTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            LastNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            FirstNameTextBox.Background = Brushes.AliceBlue;
            FirstNameTextBox.Text = "";
        }

        private void LastNameTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FirstNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.AliceBlue;
            LastNameTextBox.Text = "";
        }

        private void EmailTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FirstNameTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.White;
            PasswordTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.AliceBlue;
            EmailTextBox.Text = "";
        }

        private void PasswordTextBox_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FirstNameTextBox.Background = Brushes.White;
            LastNameTextBox.Background = Brushes.White;
            EmailTextBox.Background = Brushes.White;            
            PasswordTextBox.Background = Brushes.AliceBlue;
            PasswordTextBox.Text = "";
        }
    }
}