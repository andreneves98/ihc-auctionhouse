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
        public AuctionHouse ah;
        public MainWindow()
        {
            InitializeComponent();

            MainStackPanel.Visibility = Visibility.Collapsed;

            LoginGrid.Visibility = Visibility.Visible;
            LoginError.Visibility = Visibility.Collapsed;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            switch (index){
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
            }
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_username_hint.Visibility = Visibility.Visible;
            if (TextBox_username.Text.Length > 0){
                TextBox_username_hint.Visibility = Visibility.Hidden;
            }
        }

        private void PasswordBox_text_PasswordChanged(object sender, RoutedEventArgs e)
        {
            TextBox_password_hint.Visibility = Visibility.Visible;
            if (PasswordBox_text.Password.Length > 0)
            {
                TextBox_password_hint.Visibility = Visibility.Hidden;
            }
        }

        private void Entrar_Button_Click(object sender, RoutedEventArgs e)
        {
            String Username = "teste";
            String Password = "teste";

            // check password
            if(TextBox_username.Text == Username && PasswordBox_text.Password == Password)
            {
                // change to main window
                InitAuctionHouse("Artur Filipe");
                ListViewItem_Home.IsSelected = true; // first tab is Home
                LoginGrid.Visibility = Visibility.Collapsed;
                MainStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                // display error
                LoginError.Visibility = Visibility.Collapsed;
                LoginError.Visibility = Visibility.Visible;
            }
        }

        public void TerminarSessao_Button_Click() // to be used by "Perfil" window
        {
            // first tab is Home
            ListViewItem_Perfil.IsSelected = false;
            ListViewItem_Home.IsSelected = true;
            MainStackPanel.Visibility = Visibility.Collapsed;

            LoginGrid.Visibility = Visibility.Visible;
            LoginError.Visibility = Visibility.Collapsed;
            TextBox_username.Text = "";
            PasswordBox_text.Password = "";
        }
        
        private void InitAuctionHouse(String username)
        {
            String[] Categorias = {"Electrodomésticos", "Telemóveis", "Escritório", "Automóveis"};

            ah = new AuctionHouse(username);
            Leilao l = new Leilao("Jantes", "Jantes para um carro", "Aberto", Categorias[3], DateTime.Now.AddDays(5), "images/jantes.jpg");
            l.addLicitacao(new Licitacao("Joaquim Trindade", 53));
            l.addLicitacao(new Licitacao("João Almeida", 120));
            l.addLicitacao(new Licitacao("André Silva", 80));
            ah.addLeilao(l);

            l = new Leilao("Máquina de lavar roupa", "em segunda mão", "Aberto", Categorias[0], DateTime.Now.AddDays(2), "images/maquina.jpg");
            l.addLicitacao(new Licitacao("Maria Alves", 350));
            l.addLicitacao(new Licitacao("Pedro Nogueira", 455));
            ah.addLeilao(l);

            l = new Leilao("Microondas", "microondas novo", "Aberto", Categorias[0], DateTime.Now.AddDays(10), "images/microondas.jpg");
            l.addLicitacao(new Licitacao("Maria Alves", 70));
            l.addLicitacao(new Licitacao("Pedro Nogueira", 90));
            ah.addLeilao(l);

            l = new Leilao("IPhone 6S", "Usado", "Aberto", Categorias[1], DateTime.Now.AddDays(1), "images/iphone6s.jpg");
            l.addLicitacao(new Licitacao("João Almeida", 352));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 370));
            ah.addLeilao(l);

            l = new Leilao("Cadeira", "Usado", "Fechado", Categorias[2], DateTime.Now.AddDays(-1), "images/cadeira.jpg");
            l.addLicitacao(new Licitacao("João Almeida", 352));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 370));
            ah.addLeilao(l);

            // init User's licitações
            // ah.addUser_Licitacoes();
        }
    }
}
