﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace auctionhouse
{
    /// <summary>
    /// Interaction logic for Leiloes_leilao.xaml
    /// </summary>
    public partial class Leiloes_leilao : UserControl
    {
        private Leilao leilao;
        private Leiloes leiloes_window;
        private Licitacoes licitacoes_window;
        private Leilao Meuleilao;
        private MeusLeiloes Meusleiloes_window;

        public Leiloes_leilao(Leiloes window, Leilao l)
        {
            InitializeComponent();

            leiloes_window = window;
            leilao = l;

            initLeilaoCard(l);
        }

        public Leiloes_leilao(Licitacoes window, Leilao l)
        {
            InitializeComponent();

            licitacoes_window = window;
            leilao = l;

            initLeilaoCard(l);
        }

        public Leiloes_leilao(MeusLeiloes window, Leilao l)
        {
            InitializeComponent();

            Meusleiloes_window = window;
            Meuleilao = l;

            initLeilaoCard(l);
        }

        private void initLeilaoCard(Leilao l)
        {
            nome.Text = l.Nome;
            desc.Text = l.Descricao;
            estado.Text = l.Estado;

            lei_owner.Text = l.Owner;

            if (l.Estado == "Aberto")
            {
                estado.Foreground = Brushes.Green;
            }
            else // Fechado
            {
                estado.Foreground = Brushes.PaleVioletRed;
            }

            if (l.hasLicitacoes())
            {
                licit.Text = "Ultima licitação:";
                licit_text.Text = " " + l.getCurrentValue() + " €";
            }
            else
            {
                licit.Text = "Valor inicial:";
                licit_text.Text = " " + l.getCurrentValue() + " €";
            }

            String t = l.timeToEnd();
            if (t == "")
            {
                tempo_text.Text = "0d 00:00:00h " + t;
            }
            else
            {
                tempo_text.Text = " " + t;
            }

            img.Source = new BitmapImage(new Uri(l.imgPath, UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (leiloes_window == null && licitacoes_window == null)
            {
                Meusleiloes_window.Inspect_Button_Click(Meuleilao);
            }
            else if(licitacoes_window == null && Meusleiloes_window == null)
            {
                leiloes_window.Inspect_Button_Click(leilao);
            }
            else
            {
                licitacoes_window.Inspect_Button_Click(leilao);
            }
        }
    }
}
