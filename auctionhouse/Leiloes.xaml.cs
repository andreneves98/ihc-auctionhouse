using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Leiloes : UserControl
    {
        public Leiloes()
        {
            InitializeComponent();
        }

        private void Inspect_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            InspectGrid.Visibility = Visibility.Visible;
            SearchGrid.Visibility = Visibility.Collapsed;
        }

        private void Inspect_Back_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchGrid.Visibility = Visibility.Visible;
            InspectGrid.Visibility = Visibility.Collapsed;
        }
        
    }
}