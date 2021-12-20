using CompanyManager.Core;
using CompanyManager.View;
using CompanyManager.View.Pages;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CompanyManager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Employee? currentUser;
        private bool isLogin = true;
        private Page? curentPage;
        private Page? hallsPage;
        private Page? employeePage;
        private Page? filmPage;
        private Page? sessionPage;
        public MainViewModel()
        {

            var autWind = App.provider.GetService<AutorizationWindow>();
            var res = autWind?.ShowDialog();
            if (res == true)
            {
                currentUser = autWind?.employee;
                isLogin = true;
            }
        }
        public Employee? CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; base.OnPropertyChanged("CurrentUser"); }
        }
        public Page CurentPage
        {
            get { return curentPage; }
            set { curentPage = value; base.OnPropertyChanged("CurentPage"); }
        }
        #region Halls
        private ICommand hallsPageCommand;
        public ICommand HallsPageCommand
        {
            get { if (hallsPageCommand == null) hallsPageCommand = new RelayCommand(SetHallsPageExecute, AdministratorCanExecute); return hallsPageCommand; }
        }

        private void SetHallsPageExecute(object obj)
        {
            if (hallsPage == null) hallsPage = App.provider.GetService<HallsPage>();
            CurentPage = hallsPage;
        }
        #endregion
        #region Employees
        private ICommand employeesPageCommand;
        public ICommand EmployeesPageCommand
        {
            get { if (employeesPageCommand == null) employeesPageCommand = new RelayCommand(SetEmployeesPageExecute, AdministratorCanExecute); return employeesPageCommand; }
        }
        private void SetEmployeesPageExecute(object obj)
        {
            if (employeePage == null) employeePage = App.provider.GetService<EmployeesPage>();
            CurentPage = employeePage;
        }
        #endregion
        #region Films
        private ICommand filmsPageCommand;
        public ICommand FilmPageCommand
        {
            get { if (filmsPageCommand == null) filmsPageCommand = new RelayCommand(SetFilmsPageExecute, AdministratorCanExecute); return filmsPageCommand; }
        }
        private void SetFilmsPageExecute(object obj)
        {
            if (filmPage == null) filmPage = App.provider.GetService<FilmsPage>();
            CurentPage = filmPage;
        }
        #endregion
        #region Session
        private ICommand sesionPageCommand;
        public ICommand SesionPageCommand
        {
            get { if (sesionPageCommand == null) sesionPageCommand = new RelayCommand(SetSessionPageExecute, DefaultCanExecute); return sesionPageCommand; }
        }
        private void SetSessionPageExecute(object obj)
        {
            if (sessionPage == null) sessionPage = App.provider.GetService<SessionPage>();
            CurentPage = sessionPage;
        }
        #endregion








        private bool AdministratorCanExecute(object obj)
        {
            if (isLogin && CurrentUser?.Role == "Administrator") return true;
            return false;
        }
        private bool DefaultCanExecute(object obj)
        {
            if (isLogin) return true;
            return false;
        }
    }
}
