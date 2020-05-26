using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Leiloes : UserControl
    {
        private AuctionHouse ahref;

        public Leiloes()
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;

            Leiloes_StackPanel.Children.Clear();

            Leilao[] leiloes = ahref.getLeiloes("","Todos",-1); 
            foreach(Leilao l in leiloes)
            {
                Leiloes_StackPanel.Children.Add(new Leiloes_leilao(this, l));
            }
        }

        public void Inspect_Button_Click()
        {
            InspectGrid.Visibility = Visibility.Visible;
            SearchGrid.Visibility = Visibility.Collapsed;
        }

        public void Inspect_Back_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchGrid.Visibility = Visibility.Visible;
            InspectGrid.Visibility = Visibility.Collapsed;
        }
        
    }
}