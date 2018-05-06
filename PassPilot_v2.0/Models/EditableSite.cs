using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using PassPilot_v2._0.Services;

namespace PassPilot_v2._0.Models
{
    public class EditableSite : ValidatableBindableBase
    {
        //SetProperty(ref _siteName, value);
        private string _siteName;
        [Required]
        public string SiteName
        {
            get { return _siteName; }
            set { SetProperty(ref _siteName, value); }
        }

        private string _siteURL;
        [Url]
        public string SiteURL
        {
            get { return _siteURL; }
            set { SetProperty(ref _siteURL, value); }
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

        private string _allowedSymbols;
        public string AllowedSymbols
        {
            get { return _allowedSymbols; }
            set { _allowedSymbols = value; }
        }


        private byte _passwordLength;
        public byte PasswordLength
        {
            get { return _passwordLength; }
            set { _passwordLength = value; }
        }

        public static string DefaultSymbols = "!$%&*+,-.:;=?@^_";
        public const byte DEFAULT_PASS_LENGTH = 20;

        private ObservableCollection<CharSetting> _characterConfig;

        public ObservableCollection<CharSetting> CharacterConfig
        {
            get { return _characterConfig; }
            set { SetProperty(ref _characterConfig, value); }
        }

        public EditableSite(Site site)
        {
            SiteName = site.SiteName;
            SiteURL = site.SiteURL;
            Username = site.Username;
            Password = site.Password;
            AllowedSymbols = site.AllowedSymbols;
            PasswordLength = site.PasswordLength;
            CharacterConfig = site.CharacterConfig.DictToCharSettings();
        }

        public EditableSite()
        {
            SiteName = "";
            SiteURL = "";
            Username = "";
            Password = "";
            AllowedSymbols = DefaultSymbols;
            PasswordLength = DEFAULT_PASS_LENGTH;
            CharacterConfig = new ObservableCollection<CharSetting>()
            {
                new CharSetting("Lowercase Letters", true),
                new CharSetting("Uppercase Letters", true),
                new CharSetting("Numbers", true),
                new CharSetting("Symbols", true),
            };
        }
    }

    //maybe change the location of this later
    public class CharSetting : BindableBase
    {
        public string Label { get; set; }
        private bool _isEnabled;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
            //set { _isEnabled = value; }
        }


        public CharSetting(string lbl, bool enabled)
        {
            Label = lbl;
            IsEnabled = enabled;
        }
    }
}
