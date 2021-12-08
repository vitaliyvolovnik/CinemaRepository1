using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CompanyManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider provider;  
        public App()
        {
            ServiceCollection collection = new();
            ConfigureService(collection);
            provider = collection.BuildServiceProvider();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            /*var wind = (MainWindow)provider.GetService<MainWindow>();
            wind.Show();*/
            var db = new BLL.Class1();
            db.test();
        }
        private void ConfigureService(ServiceCollection collectoin)
        {
            collectoin.AddTransient<MainWindow>();
            BLL.ConfigureBLL.ConfigereService(collectoin);
        }
    }
}
