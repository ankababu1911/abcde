using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abcde.Mobile.Helpers
{
    internal class StoredUserData
    {
        public string AccessToken { get; private set; }

        public string AccessType { get; private set; }

        public void UpdateLoginData(string token)
        {
            AccessToken = token;
            AccessType = "Bearer";
        }
    }
}
