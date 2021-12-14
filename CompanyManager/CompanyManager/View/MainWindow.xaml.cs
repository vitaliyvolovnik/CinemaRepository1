using BLL.Services;
using CompanyManager.View.Pages;
using DLL.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CompanyManager.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Employee employee;
        
        
        public MainWindow()
        {
            InitializeComponent();
            
            var autWind = App.provider.GetService<AutorizationWindow>();
            var res = autWind.ShowDialog();
            if (res == true)
            {
                
                employee = autWind.employee;
                NameTExtBox.Content = employee.Name;
                SurnameTextBox.Content = employee.Surname;
                PostTextBox.Content = employee.Role;
            }
            else
               this.Close();
            var page = App.provider.GetService<HallsPage>();
            MainPagesFrame.Content = page;
           
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            var panel = (StackPanel)sender;
            var grid = (Grid)panel.Children[0];
            grid.Width = 0;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var panel = (StackPanel)sender;
            var grid = (Grid)panel.Children[0];
            grid.Width = 7;
        }

        

        
    }
}
