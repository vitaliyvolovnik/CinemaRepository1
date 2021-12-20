using CompanyManager.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CompanyManager.View.Pages
{
    /// <summary>
    /// Interaction logic for SessionPage.xaml
    /// </summary>
    public partial class SessionPage : Page
    {
        private readonly SessionViewModel _sessionViewModel;
        public SessionPage(SessionViewModel viewModel)
        {
            InitializeComponent();
            this._sessionViewModel = viewModel;
            this.DataContext = this._sessionViewModel;
        }
    }
}
