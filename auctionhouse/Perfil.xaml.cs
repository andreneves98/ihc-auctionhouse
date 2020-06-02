using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Perfil : UserControl
    {
        private AuctionHouse ahref;
        public Perfil()
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;


        }

        private void TerminarSessao_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.TerminarSessao_Button_Click();
        }
    }
}