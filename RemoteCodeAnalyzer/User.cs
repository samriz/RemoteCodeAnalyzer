using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteCodeAnalyzer
{
    public class User
    {
        private string firstName, lastName, email, password;
        
        public User(){}
        public User(string email, string password)
        {
            this.firstName = "";
            this.lastName = "";
            this.email = email;
            this.password = password;
        }
        public User(string firstName, string lastName, string email, string password)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.password = password;
        }

        public string GetFirstName() => firstName;
        public string GetLastName() => lastName;
        public string GetEmail() => email;
        public string GetPassword() => password;
        public void SetFirstName(string firstName) => this.firstName = firstName;
        public void SetLastName(string lastName) => this.lastName = lastName;
        public void SetEmail(string email) => this.email = email;
        public void SetPassword(string password) => this.password = password;
    }
}