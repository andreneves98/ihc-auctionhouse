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

namespace auctionhouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ((ListViewItem) FindName("home")).IsSelected = true; // first tab is Home
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index)
            {
                case 0: // Home
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Home());
                    break;
                case 1: // Leilões
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Leiloes());
                    break;
                case 2: // Licitações
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Licitacoes());
                    break;
                case 3: // Meus Leilões
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MeusLeiloes());
                    break;
                case 4: // Perfil
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Perfil());
                    break;
                Default:
                    break;
            }
        }
    }
}
