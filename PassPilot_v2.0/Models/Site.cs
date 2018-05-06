using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PassPilot_v2._0.Models
{
    public class Site : BindableBase
    {
        //SetProperty(ref _siteURL, value);
        private string _siteName;
        [Key]
        public string SiteName
        {
            get { return _siteName; }
            set { _siteName = value; }
        }

        private string _siteURL;
        public string SiteURL
        {
            get { return _siteURL; }
            set { _siteURL = value; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private byte _passwordLength;
        public byte PasswordLength
        {
            get { return _passwordLength; }
            set { _passwordLength = value; }
        }


        private string _allowedSymbols;
        public string AllowedSymbols
        {
            get { return _allowedSymbols; }
            set { _allowedSymbols = value; }
        }

        private Dictionary<string, bool> _characterConfig;
        public Dictionary<string, bool> CharacterConfig
        {
            get { return _characterConfig; }
            set { _characterConfig = value; }
        }



        private List<string> _passwordHistory;
        public List<string> PasswordHistory
        {
            get { return _passwordHistory; }
            set { _passwordHistory = value; }
        }

        private DateTime _lastChanged;

        public DateTime LastChanged
        {
            get { return _lastChanged; }
            set { _lastChanged = value; }
        }

        public Site(
            string siteName,
            string siteURL,
            string username,
            string password = "",
            string symbols = "!$%&*+,-.:;=?@^_",
            byte numOfChars = 20)
        {
            SiteName = siteName;
            SiteURL = siteURL;
            Username = username;
            Password = password;
            PasswordHistory = new List<string>();
            LastChanged = DateTime.Now;
            AllowedSymbols = symbols;
            PasswordLength = numOfChars;
            CharacterConfig = new Dictionary<string, bool>()
            {
                {"Lowercase Letters", true },
                {"Uppercase Letters", true },
                {"Numbers", true },
                {"Symbols", true }
            };

            if (AllowedSymbols == "") CharacterConfig["Symbols"] = false;
        }

        [JsonConstructor]
        public Site(
            string siteName, 
            string siteURL,
            string username,
            string password, 
            List<string> passwordHistory,
            string lastChanged,
            string symbols,
            byte passLength,
            Dictionary<string, bool> charSettings)
        {
            SiteName = siteName;
            SiteURL = siteURL;
            Username = username;
            Password = password;
            PasswordHistory = passwordHistory;
            LastChanged = Convert.ToDateTime(lastChanged);
            AllowedSymbols = symbols;
            PasswordLength = passLength;
            CharacterConfig = charSettings;
        }

        public override string ToString()
        {
            return SiteName;
        }
    }
}
