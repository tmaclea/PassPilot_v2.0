using PassPilot_v2._0.Models;
using PassPilot_v2._0.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace PassPilot_v2._0.ViewModels
{
    public class SiteListViewModel : BindableBase
    {
        private ISiteData _repo;

        private ObservableCollection<Site> _sites;
        public ObservableCollection<Site> Sites
        {
            get { return _sites; }
            set { SetProperty(ref _sites, value); }
        }

        private Site _selectedSite;
        public Site SelectedSite
        {
            get { return _selectedSite; }
            set
            {
                SetProperty(ref _selectedSite, value);
                DeleteCommand.RaiseCanExecuteChanged();
                CopyCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                HistoryCommand.RaiseCanExecuteChanged();
                GoToSiteCommand.RaiseCanExecuteChanged();
                GenerateCommand.RaiseCanExecuteChanged();
                CopyText = "Copy Password";
            }
        }

        //this doesn't feel MVVM friendly
        private string _copyText;
        public string CopyText  
        {
            get { return _copyText; }
            set { SetProperty(ref _copyText, value); }
        }


        public event Action<Site> EditSiteRequested = delegate { };
        public event Action AddSiteRequested = delegate { };
        public event Action<Site> PasswordRequested = delegate { };
        public event Action<Site> HistoryRequested = delegate { };

        //commands
        public RelayCommand<Site> DeleteCommand { get; private set; }
        public RelayCommand CopyCommand { get; private set; }
        public RelayCommand<Site> EditCommand { get; private set; }
        public RelayCommand AddCommand { get; private set; }
        public RelayCommand<Site> GenerateCommand { get; private set; }
        public RelayCommand<Site> HistoryCommand { get; private set; }
        public RelayCommand<Site> GoToSiteCommand { get; private set; }

        //_repo should be inserted into here via dependancy injection
        public SiteListViewModel(ISiteData repo)
        {
            _repo = repo;
            UpdateSiteList();

            DeleteCommand = new RelayCommand<Site>(OnDelete, CanDelete);
            CopyCommand = new RelayCommand(OnCopy, CanCopy);
            EditCommand = new RelayCommand<Site>(OnEdit, CanEdit);
            AddCommand = new RelayCommand(OnAdd);
            GenerateCommand = new RelayCommand<Site>(OnGenerate, CanGenerate);
            HistoryCommand = new RelayCommand<Site>(OnHistory, CanHistory);
            GoToSiteCommand = new RelayCommand<Site>(OnGoToSite, CanGoToSite);
            CopyText = "Copy Password";
        }

        public void UpdateSiteList()
        {
            Sites = new ObservableCollection<Site>(_repo.GetSites());
        }

        private void OnGoToSite(Site site)
        {
            System.Diagnostics.Process.Start(site.SiteURL);
        }
        private bool CanGoToSite(Site site)
        {
            return SelectedSite != null;
        }

        private void OnHistory(Site site)
        {
            HistoryRequested(site);
        }
        private bool CanHistory(Site site)
        {
            return SelectedSite != null;
        }

        private void OnGenerate(Site site)
        {
            PasswordRequested(site);
        }
        private bool CanGenerate(Site site)
        {
            return SelectedSite != null;
        }

        private void OnAdd()
        {
            AddSiteRequested();
        }

        private void OnDelete(Site site)
        {
            //Is this MVVM friendly?
            var result = MessageBox.Show(
                "You are about to permanently delete " + site.SiteName + 
                "\nAre you sure you want to do this?",
                "Warning!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                _repo.Delete(site);
                UpdateSiteList();
            }
            else return;
        }
        private bool CanDelete(Site site)
        {
            return SelectedSite != null;
        }

        private void OnCopy()
        {
            Clipboard.SetText(SelectedSite.Password);
            CopyText = "Copied!";
        }
        private bool CanCopy()
        {
            return SelectedSite != null;
        }

        private void OnEdit(Site site)
        {
            EditSiteRequested(site);
        }
        private bool CanEdit(Site site)
        {
            return SelectedSite != null;
        }
    }
}
