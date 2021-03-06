﻿using System;
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
                case 0: // Leilões
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Leiloes());
                    break;
                case 1: // Licitações
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new Licitacoes("Artur Filipe"));
                    break;
                case 2: // Meus Leilões
                    ContentGrid.Children.Clear();
                    ContentGrid.Children.Add(new MeusLeiloes("Artur Filipe"));
                    break;
                case 3: // Perfil
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
            String Username = "arturfilipe";
            String Password = "arturf";
            String fullName = "Artur Filipe";

            // check password
            if (TextBox_username.Text == Username && PasswordBox_text.Password == Password)
            {
                // change to main window
                InitAuctionHouse(fullName);
                Menu_username.Text = fullName;
                ListViewItem_Leiloes.IsSelected = true; // first tab is Leiloes
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
            // first tab is leiloes
            ListViewItem_Perfil.IsSelected = false;
            ListViewItem_Leiloes.IsSelected = true;
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

            ah.addUser("Joaquim Trindade", 5);
            ah.addUser("João Almeida", 2);
            ah.addUser("André Silva", 1);
            ah.addUser("Maria Alves", 5);
            ah.addUser("Pedro Nogueira", 3);
            ah.addUser("Artur Filipe", 4);
            ah.addUser("Miguel Costa", 3);
            ah.addUser("Bruno Pereira", 4);

            Leilao l = new Leilao("Jantes", "Jantes para um carro", "Aberto", Categorias[3], DateTime.Now.AddDays(5), "images/jantes.jpg", "Artur Filipe", 25.3);
            l.addLicitacao(new Licitacao("Joaquim Trindade", 53));
            l.addLicitacao(new Licitacao("João Almeida", 120));
            l.addLicitacao(new Licitacao("André Silva", 80));
            ah.addLeilao(l);

            l = new Leilao("Máquina de lavar roupa", "em segunda mão", "Aberto", Categorias[0], DateTime.Now.AddDays(2), "images/maquina.jpg", "Miguel Costa", 339);
            ah.addLeilao(l);

            l = new Leilao("Microondas", "microondas novo", "Aberto", Categorias[0], DateTime.Now.AddDays(10), "images/microondas.jpg", "Bruno Pereira", 59.99);
            l.addLicitacao(new Licitacao("Maria Alves", 70));
            l.addLicitacao(new Licitacao("Artur Filipe", 75)); // uncomment to show empty licitacoes
            l.addLicitacao(new Licitacao("Pedro Nogueira", 90));
            ah.addLeilao(l);

            l = new Leilao("IPhone 6S", "Usado", "Aberto", Categorias[1], DateTime.Now.AddDays(1), "images/iphone6s.jpg", "Artur Filipe", 299.50);
            l.addLicitacao(new Licitacao("João Almeida", 352));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 370));
            l.addLicitacao(new Licitacao("João Almeida", 389));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 400));
            l.addLicitacao(new Licitacao("João Almeida", 420));
            ah.addLeilao(l);

            l = new Leilao("Cadeira", "Usado", "Fechado", Categorias[2], DateTime.Now.AddDays(-1), "images/cadeira.jpg", "Artur Filipe", 350);
            l.addLicitacao(new Licitacao("João Almeida", 352));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 370));
            l.addLicitacao(new Licitacao("João Almeida", 390));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 400));
            l.addLicitacao(new Licitacao("João Almeida", 450));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 500));
            l.addLicitacao(new Licitacao("João Almeida", 550));
            l.addLicitacao(new Licitacao("Joaquim Trindade", 600));
            ah.addLeilao(l);

            // init User's licitações
            // ah.addUser_Licitacoes();
        }
    }
}
