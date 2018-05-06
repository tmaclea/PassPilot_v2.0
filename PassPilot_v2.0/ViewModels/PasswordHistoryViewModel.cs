using PassPilot_v2._0.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassPilot_v2._0.ViewModels
{
    public class PasswordHistoryViewModel : BindableBase
    {
        private ObservableCollection<string> _history;
        public ObservableCollection<string> History
        {
            get { return _history; }
            set { SetProperty(ref _history, value); }
        }

        private string _selectedPassword;
        public string SelectedPassword
        {
            get { return _selectedPassword; }
            set
            {
                SetProperty(ref _selectedPassword, value);
                CopyCommand.RaiseCanExecuteChanged();
                CopyText = "Copy Password";
            }
        }

        //Again, does not feel MVVM friendly
        private string _copyText;
        public string CopyText
        {
            get { return _copyText; }
            set { SetProperty(ref _copyText, value); }
        }

        public event Action BackRequested = delegate { };

        public RelayCommand BackCommand { get; private set; }
        public RelayCommand CopyCommand { get; private set; }

        public PasswordHistoryViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
                return;

            BackCommand = new RelayCommand(OnBack);
            CopyCommand = new RelayCommand(OnCopy, CanCopy);

            CopyText = "Copy Password";
        }

        private void OnCopy()
        {
            Clipboard.SetText(SelectedPassword);
            CopyText = "Copied!";
        }
        private bool CanCopy()
        {
            return SelectedPassword != null;
        }

        private void OnBack()
        {
            BackRequested();
            SelectedPassword = null;
        }
    }
}
