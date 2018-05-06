using PassPilot_v2._0.Models;
using PassPilot_v2._0.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PassPilot_v2._0.ViewModels
{
    public class PasswordSymbolsViewModel : BindableBase
    {
        private ISiteData _repo;

        private Site _siteSave;
        public Site SiteSave
        {
            get { return _siteSave; }
            set
            {
                _siteSave = value;
            }
        }


        private EditableSite _siteCopy;
        public EditableSite SiteCopy
        {
            get { return _siteCopy; }
            set
            {
                _siteCopy = value;
                Included = new ObservableCollection<char?>(_siteCopy.AllowedSymbols.ToNullableCharArray().ToList());
                Excluded = new ObservableCollection<char?>((AllSymbols.ToNullableCharArray()).Except(Included).ToList());
            }
        }

        private const string AllSymbols = "!$%&*+,-.:;=?@^_\"\\#()`|~;{}[]<>'";

        private ObservableCollection<char?> _included;
        public ObservableCollection<char?> Included
        {
            get { return _included; }
            set { SetProperty(ref _included, value); }
        }

        private ObservableCollection<char?> _excluded;
        public ObservableCollection<char?> Excluded
        {
            get { return _excluded; }
            set { SetProperty(ref _excluded, value); }
        }

        private char? _selectedInclude;
        public char? SelectedInclude
        {
            get { return _selectedInclude; }
            set
            {
                SetProperty(ref _selectedInclude, value);
                ExcludeCommand.RaiseCanExecuteChanged();
            }
        }

        private char? _selectedExclude;
        public char? SelectedExclude
        {
            get { return _selectedExclude; }
            set
            {
                SetProperty(ref _selectedExclude, value);
                IncludeCommand.RaiseCanExecuteChanged();
            }
        }

        public event Action<Site> Done = delegate { };
        public event Action DefaultRequested = delegate { };
        public event Action IncludeAllRequested = delegate { };

        public RelayCommand<object> SaveCommand { get; private set; }
        public RelayCommand<char?> IncludeCommand { get; private set; }
        public RelayCommand<char?> ExcludeCommand { get; private set; }
        public RelayCommand DefaultCommand { get; private set; }
        public RelayCommand IncludeAllCommand { get; private set; }

        public PasswordSymbolsViewModel(ISiteData repo)
        {
            _repo = repo;

            Included = new ObservableCollection<char?>();
            Excluded = new ObservableCollection<char?>();

            SaveCommand = new RelayCommand<object>(OnSave);
            IncludeCommand = new RelayCommand<char?>(OnInclude, CanInclude);
            ExcludeCommand = new RelayCommand<char?>(OnExclude, CanExclude);
            DefaultCommand = new RelayCommand(OnDefault);
            IncludeAllCommand = new RelayCommand(OnIncludeAll);
        }

        private void OnIncludeAll()
        {
            Included = AllSymbols.ToNullableCharArray();
            Excluded = "".ToNullableCharArray();
        }

        public void OnDefault()
        {
            Included = EditableSite.DefaultSymbols.ToNullableCharArray();
            Excluded = new ObservableCollection<char?>((AllSymbols.ToNullableCharArray()).Except(Included).ToList());
        }

        private void OnInclude(char? symbol)
        {
            Excluded.Remove(symbol);
            Included.Insert(0, symbol);
        }
        private bool CanInclude(char? symbol)
        {
            return symbol != null;
        }

        private void OnExclude(char? symbol)
        {
            Included.Remove(symbol);
            Excluded.Insert(0, symbol);
        }
        private bool CanExclude(char? symbol)
        {
            return symbol != null;
        }

        private void OnSave(object sites)
        {
            var values = (object[])sites;
            var edit = (EditableSite)values[0];
            edit.AllowedSymbols = GetString(Included);
            _repo.Update(edit, (Site)values[1]);
            Done((Site)values[1]);
        }

        private string GetString(ObservableCollection<char?> nullCharCollection)
        {
            string charStr = "";
            foreach(char? character in nullCharCollection)
            {
                if (character == null) continue;
                charStr += character;
            }
            return charStr;
        }
    }
}