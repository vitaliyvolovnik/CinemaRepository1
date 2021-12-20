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
    /// Interaction logic for FilmsPage.xaml
    /// </summary>
    public partial class FilmsPage : Page
    {
        private readonly FilmViewModel _filmViewModel;
        public FilmsPage(FilmViewModel model)
        {
            InitializeComponent();
            this._filmViewModel = model;
            this.DataContext = this._filmViewModel;
            FilmListBox.ItemsSource = _filmViewModel.Films;

        }
    }
}
