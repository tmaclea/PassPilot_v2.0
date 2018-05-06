using PassPilot_v2._0.Models;
using PassPilot_v2._0.Services;
using System;
using System.ComponentModel;

namespace PassPilot_v2._0.ViewModels
{
    public class SiteAddEditViewModel : BindableBase
    {
        private ISiteData _repo;

        public bool EditFlag { get; set; }

        private Site _originalSite;

        public Site OriginalSite
        {
            get { return _originalSite; }
            set
            {
                _originalSite = value;
                AddEditSite = new EditableSite(_originalSite);
            }
        }


        private EditableSite _addEditSite;
        public EditableSite AddEditSite
        {
            get { return _addEditSite; }
            set
            {
                SetProperty(ref _addEditSite, value);
                //Remove the events if the site is null to avoid memory leaks
                if (_addEditSite == null) _addEditSite.ErrorsChanged -= RaiseCanExecuteChanged;
                _addEditSite.ErrorsChanged += RaiseCanExecuteChanged;
            }
        }
        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
            GenerateCommand.RaiseCanExecuteChanged();
        }

        public event Action Done = delegate { };
        public event Action<Site> PasswordRequested = delegate { };

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand GenerateCommand { get; private set; }

        public SiteAddEditViewModel(ISiteData repo)
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            _repo = repo;

            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
            GenerateCommand = new RelayCommand(OnGenerate, CanGenerate);
        }

        private void OnGenerate()
        {
            if(EditFlag)
            {
                _repo.Update(AddEditSite, OriginalSite);
                PasswordRequested(OriginalSite);
            }else
            {
                Site site = new Site(AddEditSite.SiteName, AddEditSite.SiteURL,
                    AddEditSite.Username, "");
                _repo.Add(site);
                PasswordRequested(site);
            }
        }
        private bool CanGenerate()
        {
            if (AddEditSite == null) return false;
            return !AddEditSite.HasErrors;
        }

        private void OnSave()
        {
            //if (AddEditSite == null) return;
            if (EditFlag)
            {
                _repo.Update(AddEditSite, OriginalSite);
            }else
            {
                Site site = new Site(AddEditSite.SiteName, AddEditSite.SiteURL,
                    AddEditSite.Username, AddEditSite.Password);
                _repo.Add(site);
            }
            Done();
        }
        private bool CanSave()
        {
            if (AddEditSite == null) return false;
            return !AddEditSite.HasErrors;
        }

        private void OnCancel()
        {
            Done();
        }
    }
}
