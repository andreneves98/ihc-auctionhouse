using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            img.Source = new BitmapImage(new Uri("images/artur_user.jpg", UriKind.Relative));

            double rating = ahref.getRating(ahref.getUsername());
            int i = 0;
            foreach (UIElement elem in Inspect_lei_rating.Children)
            {
                if (i < rating)
                {
                    ((MaterialDesignThemes.Wpf.PackIcon)elem).Foreground = Brushes.Gold;
                }
                else
                {
                    ((MaterialDesignThemes.Wpf.PackIcon)elem).Foreground = Brushes.LightGray;
                }
                i++;
            }
        }

        private void TerminarSessao_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult m_res = MessageBox.Show("Terminar Sessão?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (m_res == MessageBoxResult.Yes)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.TerminarSessao_Button_Click();
            }
        }
    }
}