using PassPilot_v2._0.Models;
using PassPilot_v2._0.Services;
using PassPilot_v2._0.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Unity;

namespace PassPilot_v2._0
{
    public class MainWindowViewModel : BindableBase
    {
        private SiteListViewModel _siteListViewModel;
        private PasswordChangeViewModel _passwordChangeViewModel;
        private SiteAddEditViewModel _siteAddEditViewModel;
        private PasswordSymbolsViewModel _passwordSymbolsViewModel;
        private PasswordHistoryViewModel _passwordHistoryViewModel = new PasswordHistoryViewModel();

        private BindableBase _currentViewModel;

        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }

        public MainWindowViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            _siteListViewModel = ContainerHelper.Container.Resolve<SiteListViewModel>();
            _passwordChangeViewModel = ContainerHelper.Container.Resolve<PasswordChangeViewModel>();
            _siteAddEditViewModel = ContainerHelper.Container.Resolve<SiteAddEditViewModel>();
            _passwordSymbolsViewModel = ContainerHelper.Container.Resolve<PasswordSymbolsViewModel>();

            CurrentViewModel = _siteListViewModel;
            _siteListViewModel.EditSiteRequested += NavToEdit;
            _siteListViewModel.AddSiteRequested += NavToAdd;
            _siteListViewModel.PasswordRequested += NavToChange;
            _siteListViewModel.HistoryRequested += NavToHistory;
            _siteAddEditViewModel.Done += NavToList;
            _siteAddEditViewModel.PasswordRequested += NavToChange;
            _passwordHistoryViewModel.BackRequested += NavToList;
            _passwordChangeViewModel.Done += NavToList;
            _passwordChangeViewModel.AdvancedRequested += NavToAdvanced;
            _passwordSymbolsViewModel.Done += NavToChange;
        }

        private void NavToAdvanced(EditableSite copy, Site site)
        {
            _passwordSymbolsViewModel.SiteSave = site;
            _passwordSymbolsViewModel.SiteCopy = copy;
            CurrentViewModel = _passwordSymbolsViewModel;
        }

        private void NavToHistory(Site site)
        {
            _passwordHistoryViewModel.History = new ObservableCollection<string>(site.PasswordHistory);
            CurrentViewModel = _passwordHistoryViewModel;
        }

        private void NavToChange(Site site)
        {
            _passwordChangeViewModel.SiteToChange = site;
            _passwordChangeViewModel.GetConfiguration(site);
            CurrentViewModel = _passwordChangeViewModel;
        }

        private void NavToAdd()
        {
            _siteAddEditViewModel.EditFlag = false;

            _siteAddEditViewModel.AddEditSite = new EditableSite();

            CurrentViewModel = _siteAddEditViewModel;
        }

        private void NavToEdit(Site site)
        {
            _siteAddEditViewModel.EditFlag = true;
            _siteAddEditViewModel.OriginalSite = site;
            CurrentViewModel = _siteAddEditViewModel;
        }

        private void NavToList()
        {
            _siteListViewModel.UpdateSiteList();
            CurrentViewModel = _siteListViewModel;
        }
    }
}
