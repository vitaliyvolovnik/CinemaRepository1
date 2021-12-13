using BLL.Services;
using CompanyManager.View;
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
    /// Interaction logic for AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        AutorizationService autorizationService;
        public AutorizationWindow(AutorizationService service)
        {
            InitializeComponent();
            this.autorizationService = service;
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var res = await autorizationService.AutorizationAsync(MailTextBox.Text, PasswordPasswordBox.Password);
            if (res == null) return;
            if (res.isFire) return;
            this.Close();
            var wind = App.provider.GetService<MainWindow>();
            wind.Employee = res;
            wind.Show();


        }
    }
}
