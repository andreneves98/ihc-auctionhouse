using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Perfil : UserControl
    {
        public Perfil()
        {
            InitializeComponent();
        }

        private void TerminarSessao_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.TerminarSessao_Button_Click();
        }
    }
}