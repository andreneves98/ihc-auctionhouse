using System;
using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Home : UserControl
    {
        private AuctionHouse ahref;

        public Home()
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;

            home_username.Text = ahref.getUsername() + "!";
        }
    }
}