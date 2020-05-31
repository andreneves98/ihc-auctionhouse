using System;
using System.Windows;
using System.Windows.Controls;
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
        private Leilao Meusleilao;
        private MeusLeiloes Meusleiloes_window;

        public Leiloes_leilao(Leiloes window, Leilao l)
        {
            InitializeComponent();

            leiloes_window = window;
            leilao = l;

            nome.Text = l.Nome;
            desc.Text = l.Descricao;
            estado.Text = l.Estado;

            licit.Text = "Ultima licitação: " + l.highestLicitacao() + " €";

            String t = l.timeToEnd();
            if (t == "")
            {
                tempo.Text = "";
            }
            else
            {
                tempo.Text = "Tempo restante: " + t;
            }

            img.Source = new BitmapImage(new Uri(l.imgPath, UriKind.Relative));
        }

        public Leiloes_leilao(MeusLeiloes window, Leilao l)
        {
            InitializeComponent();

            Meusleiloes_window = window;
            Meusleilao = l;

            nome.Text = l.Nome;
            desc.Text = l.Descricao;
            estado.Text = l.Estado;

            licit.Text = "Ultima licitação: " + l.highestLicitacao() + " €";

            String t = l.timeToEnd();
            if (t == "")
            {
                tempo.Text = "";
            }
            else
            {
                tempo.Text = "Tempo restante: " + t;
            }

            img.Source = new BitmapImage(new Uri(l.imgPath, UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            leiloes_window.Inspect_Button_Click(leilao);
        }
    }
}
