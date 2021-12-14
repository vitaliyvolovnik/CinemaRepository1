﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CompanyManager.View;
using BLL.Services;
using CompanyManager.ViewModel;
using CompanyManager.View.Pages;

namespace CompanyManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static public IServiceProvider provider;  
        public App()
        {
            ServiceCollection collection = new();
            ConfigureService(collection);
            provider = collection.BuildServiceProvider();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wind = provider.GetService<MainWindow>();
            wind.Show();




        }
        private void ConfigureService(ServiceCollection collectoin)
        {
            collectoin.AddTransient<AutorizationWindow>();
            collectoin.AddTransient<MainWindow>();
            collectoin.AddTransient<HallsPage>();
            collectoin.AddTransient<TicketService>();
            collectoin.AddTransient<HallsViewModel>();
            collectoin.AddTransient<EmployeeService>();
            collectoin.AddTransient<AutorizationService>();
            collectoin.AddTransient<AdministrationService>();
            collectoin.AddTransient<SessionHallService>();

            BLL.ConfigureBLL.ConfigereService(collectoin);
        }
    }
}
