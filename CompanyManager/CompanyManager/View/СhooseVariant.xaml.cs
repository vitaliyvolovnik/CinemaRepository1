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
    /// Interaction logic for СhooseVariant.xaml
    /// </summary>
    public partial class СhooseVariant : Window
    {
        public СhooseVariant(string text,bool IsBooking, bool IsPaid)
        {
            InitializeComponent();
            Text.Content = text;
            if (IsBooking) { 
                BookingBtn.IsEnabled = false;
            }
            else
            {
                CancelBtn.IsEnabled = false;
            }
            if(IsPaid)
            {
                BookingBtn.IsEnabled = false;
                PaidBtn.IsEnabled = false;
            }

        }
        public int state = 0;
        private void BookingBtn_Click(object sender, RoutedEventArgs e)
        {
            state = 1;
            DialogResult=true;
            this.Close();
        }
        private void PaidBtn_Click(object sender, RoutedEventArgs e)
        {
            state = 2;
            DialogResult = true;
            this.Close();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            state = 3;
            DialogResult = true;
            this.Close();
        }
    }
}
