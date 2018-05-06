using PassPilot_v2._0.Models;
using PassPilot_v2._0.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace PassPilot_v2._0.ViewModels
{
    public class PasswordChangeViewModel : BindableBase
    {
        private ISiteData _repo;

        private Site _siteToChange;
        public Site SiteToChange
        {
            get { return _siteToChange; }
            set
            {
                _siteToChange = value;
            }
        }

        private EditableSite _siteCopy;
        public EditableSite SiteCopy
        {
            get { return _siteCopy; }
            set { SetProperty(ref _siteCopy, value); }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public event Action Done = delegate { };
        public event Action<EditableSite, Site> AdvancedRequested = delegate { };

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand<EditableSite> GenerateCommand { get; private set; }
        public RelayCommand<object> AdvancedCommand { get; private set; }
        public RelayCommand<object> SaveCommand { get; private set; }

        public PasswordChangeViewModel(ISiteData repo)
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            _repo = repo;

            CancelCommand = new RelayCommand(OnCancel);
            GenerateCommand = new RelayCommand<EditableSite>(OnGenerate, CanGenerate);
            AdvancedCommand = new RelayCommand<object>(OnAdvanced);
            SaveCommand = new RelayCommand<object>(OnSave);
        }

        public void GetConfiguration(Site site)
        {
            SiteCopy = new EditableSite(site);
            //Defaults
            Password = SiteCopy.Password;

            //foreach (var item in Includes)
            foreach(var item in SiteCopy.CharacterConfig)
            {
                item.PropertyChanged +=
                    delegate { GenerateCommand.RaiseCanExecuteChanged(); };

                if(item.Label == "Symbols" &&
                    SiteCopy != null &&
                    SiteCopy.AllowedSymbols == "")
                {
                    //clicking symbols checkbox when there are no allowed symbols will navigate
                    //user to the PasswordSymbolsView
                    item.PropertyChanged +=
                        delegate { if (SiteCopy.AllowedSymbols == "") { OnAdvanced(SiteCopy, SiteToChange); } };

                    //uncheck symbols if it's empty
                    if (SiteCopy != null || SiteCopy.AllowedSymbols == "") item.IsEnabled = false;
                }
            }
        }

        private void OnAdvanced(object sites)
        {
            var values = (object[])sites;
            AdvancedRequested((EditableSite)values[0],(Site)values[1]);
        }

        private void OnAdvanced(EditableSite copy, Site site)
        {
            AdvancedRequested(copy, site);
        }

        private void OnSave(object sites)
        {
            var values = (object[])sites;

            var oldSite = (Site)values[1];
            var updated = (EditableSite)values[0];
            updated.Password = (string)values[2];

            _repo.Update(updated,oldSite);
            Done();
        }

        private void OnGenerate(EditableSite site)
        {
            if(site.CharacterConfig.FirstOrDefault(s => s.Label == "Symbols").IsEnabled)
            {
                Password = PasswordGenerator.Generate(site.PasswordLength,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Lowercase Letters").IsEnabled,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Uppercase Letters").IsEnabled,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Numbers").IsEnabled,
                    site.AllowedSymbols);
            }
            else
            {
                Password = PasswordGenerator.Generate(site.PasswordLength,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Lowercase Letters").IsEnabled,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Uppercase Letters").IsEnabled,
                    site.CharacterConfig.FirstOrDefault(s => s.Label == "Numbers").IsEnabled,"");
            }
        }
        private bool CanGenerate(EditableSite site)
        {
            if (site == null) return true;

            bool enable = false;
            foreach (var item in site.CharacterConfig)
            {
                if (item.IsEnabled) { enable = true; break; }
            }
            return enable;
        }

        private void OnCancel()
        {
            Done();
        }

        private static class PasswordGenerator
        {
            public static byte NumOfChars { get; set; }

            private static string Lowercase =
                "abcdefghijklmnopqrstuvwxyz";
            private static string Uppercase =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            private static string Numbers =
                "0123456789";

            private static string Password { get; set; }

            public static string Generate(byte numChars, bool InclLower, bool InclUpper,
                bool InclNumbers, string symbols)
            {
                NumOfChars = numChars;

                var rand = new Random();
                Password = "";
                var includedCharacters = "";

                //also ensure that included categories are in the password
                if (InclLower)
                {
                    Password += Lowercase[rand.Next(Lowercase.Length)];
                    includedCharacters += Lowercase;
                }
                if (InclUpper)
                {
                    Password += Uppercase[rand.Next(Uppercase.Length)];
                    includedCharacters += Uppercase;
                }
                if (InclNumbers)
                {
                    Password += Numbers[rand.Next(Numbers.Length)];
                    includedCharacters += Numbers;
                }
                includedCharacters += symbols;
                if (symbols != "") Password += symbols[rand.Next(symbols.Length)];

                //shuffle Password
                Password = new string(Password.ToCharArray().OrderBy(s => (rand.Next(2) % 2) == 0).ToArray());

                while (Password.Length < numChars)
                {
                    var randomIndex = rand.Next(Password.Length);
                    var randomChar = includedCharacters[rand.Next(includedCharacters.Length)].ToString();
                    Password = Password.Insert(randomIndex, randomChar);
                }
                return Password;
            }
        }
    }
}
