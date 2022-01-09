using BLL.Services;
using CompanyManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManager.ViewModel
{
    public class ChangePassword : BaseViewModel
    {
        private readonly AutorizationService _autorizationService;
        public ChangePassword(AutorizationService autorizationService)
        {
            this._autorizationService = autorizationService;
        }

        private string email="";
        public string Email
        {
            get { return email; }
            set { email = value;base.OnPropertyChanged("Email"); }
        }
        private string pass = "";
        public string Pass
        {
            get { return pass; }
            set { pass = value; base.OnPropertyChanged("Pass"); }
        }
        private string newPass = "";
        public string NewPass
        {
            get { return newPass; }
            set { newPass = value; base.OnPropertyChanged("NewPass"); }
        }
        private string newPassRep = "";
        public string NewPassRep
        {
            get { return newPassRep; }
            set { newPassRep = value; base.OnPropertyChanged("NewPassRep"); }
        }

        private ICommand changePasswordCommand;
        public ICommand ChangePasswordCommand
        {
            get { if (changePasswordCommand == null) changePasswordCommand = new RelayCommand(ChangeCommandExecute,ChangePasswordCanExecute);return changePasswordCommand; }
        }

        private bool ChangePasswordCanExecute(object obj)
        {
            if (NewPass != NewPassRep) return false;
            if (NewPass == Pass) return false;
            if (!Regex.IsMatch(NewPass,@"\A[0-9a-zA-z-_.]{1,20}\z"))return false;
            if (string.IsNullOrWhiteSpace(Email)) return false;
            if (string.IsNullOrWhiteSpace(Pass)) return false;
            return true;
        }

        private async void ChangeCommandExecute(object obj)
        {
            if(!(await _autorizationService.ChangePasswordAsync(Email,Pass,NewPass)))return;
            Email = "";
            Pass = "";
            NewPass = "";
            NewPassRep = "";
        }
    }
}
