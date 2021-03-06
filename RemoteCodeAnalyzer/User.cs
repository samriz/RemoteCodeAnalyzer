using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteCodeAnalyzer
{
    public class User
    {
        private string email, password;
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string GetEmail() => email;
        public string GetPassword() => password;
        public void SetEmail(string email) => this.email = email;
        public void SetPassword(string password) => this.password = password;
    }
}
