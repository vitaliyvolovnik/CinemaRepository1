using BLL.Services;
using CompanyManager.Core;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManager.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        public EmployeeViewModel(AdministrationService service, EmployeeService eService)
        {
            this._administrationService = service;
            this._employeeService = eService;
            LoadEmployees();
        }
        //services
        private readonly AdministrationService _administrationService;
        private readonly EmployeeService _employeeService;

        private bool IsLoadedEmployees = false;

        //Add Person
        private Employee addEmployee;
        public Employee AddEmployee
        {
            get { if (addEmployee == null) addEmployee = new Employee(); return addEmployee; }
            set { addEmployee = value; base.OnPropertyChanged("Employee"); }
        }
        //All Worker
        private ObservableCollection<Employee> employees;
        public ObservableCollection<Employee> Employees
        {
            get { if (employees == null) employees = new(); return employees; }
        }
        //Load Employee
        public async Task LoadEmployees()
        {
            var list = await _employeeService.GetAllWhoWorkAsync();
            foreach (var item in list)
            {
                Employees.Add(item);
            }
            IsLoadedEmployees = true;
        }

        //Add Command
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { if (addCommand == null) addCommand = new RelayCommand(AddEmployeeExecute, AddEmployeeCanExecute); return addCommand; }
        }

        private bool AddEmployeeCanExecute(object obj)
        {
            if (!IsLoadedEmployees) return false;
            if (Regex.IsMatch(addEmployee.Name, "^[a-z-]{1,25}$")) return false;
            if (Regex.IsMatch(addEmployee.Surname, "^[a-z-]{1,25}$")) return false;
            if (Regex.IsMatch(addEmployee.Mail, @"^[a-z][a-z 0-9-]{1,14}@[a-z 0-9]{2,7}\.[a-z]{2,5}$")) return false;
            if (addEmployee.Role == "Administrator" || addEmployee.Role == "cashier") return false;
            var time = DateTime.Now;
            var Ctime = time.AddYears(-16);
            if (addEmployee.DayOfBirdh < Ctime) return false;
            if (addEmployee.DayOfBirdh > new DateTime(1960, 1, 1)) return false;
            
            return true;
        }

        private async void AddEmployeeExecute(object obj)
        {
            var res = await _administrationService.AddWorkerAsync(addEmployee);
            if (res == null) return;
            Employees.Add(res);
            addEmployee = new();
        }

    }
}
