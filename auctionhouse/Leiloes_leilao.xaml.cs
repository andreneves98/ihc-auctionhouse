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
using System.Windows.Shapes;

namespace auctionhouse
{
    /// <summary>
    /// Interaction logic for Leiloes_leilao.xaml
    /// </summary>
    public partial class Leiloes_leilao : UserControl
    {
        private Leilao leilao;
        private Leiloes leiloes_window;

        public Leiloes_leilao(Leiloes window,Leilao l)
        {
            InitializeComponent();

            leiloes_window = window;
            leilao = l;

            nome.Text = l.Nome;
            desc.Text = l.Descricao;
            estado.Text = l.Estado;

            licit.Text = "Ultima licitação: " + l.highestLicitacao() + " €";
            tempo.Text = "Tempo restante: " + l.timeToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            leiloes_window.Inspect_Button_Click();
        }
    }
}
