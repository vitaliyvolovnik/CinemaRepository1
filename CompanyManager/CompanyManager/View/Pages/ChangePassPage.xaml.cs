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
    /// Interaction logic for ChangePassPage.xaml
    /// </summary>
    public partial class ChangePassPage : Page
    {
        private readonly ChangePassword _changePassword;
        public ChangePassPage(ChangePassword viewModel)
        {
            InitializeComponent();
            this._changePassword = viewModel;
            this.DataContext = _changePassword;
            
        }
    }
}
