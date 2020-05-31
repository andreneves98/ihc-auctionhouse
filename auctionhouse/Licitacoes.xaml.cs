using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace auctionhouse
{
    public partial class Licitacoes : UserControl
    {
        private AuctionHouse ahref;

        public Licitacoes()
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;
            setLicitacoes();
        }

        private void setLicitacoes()
        {
            Leiloes_StackPanel.Children.Clear();

            Leilao[] userLeiloes = ahref.getUserLeiloes("Artur Filipe");
            foreach (Leilao lei in userLeiloes)
            {
                Leiloes_StackPanel.Children.Add(new Leiloes_leilao(this, lei));
            }
        }

        /*public void Inspect_Button_Click(Leilao insp_leilao)
        {
            current_insp_leilao = insp_leilao;

            init_inspect_fields();

            // clean prev error
            LicitarError.Visibility = Visibility.Collapsed;
            LicitarSuccess.Visibility = Visibility.Collapsed;

            // clear value
            licitar_text.Text = "";

            InspectGrid.Visibility = Visibility.Visible;
            SearchGrid.Visibility = Visibility.Collapsed;
        }

        public void init_inspect_fields()
        {
            // init fields
            Inspect_lei_nome.Text = current_insp_leilao.Nome;
            Inspect_lei_desc.Text = current_insp_leilao.Descricao;
            Inspect_lei_estado.Text = current_insp_leilao.Estado;
            Inspect_lei_ult_licit.Text = "Última licitação: " + current_insp_leilao.highestLicitacao().ToString() + " €";
            Inspect_lei_tempo.Text = "Tempo restante: " + current_insp_leilao.timeToEnd();
            Inspect_lei_img.Source = new BitmapImage(new Uri(current_insp_leilao.imgPath, UriKind.Relative));
        }

        public void Inspect_Back_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // refresh leiloes list
            string words = SearchWords.Text;
            string categ = ((ComboBoxItem)Categ.SelectedItem).Content.ToString();
            string sortText = ((ComboBoxItem)SortPrice.SelectedItem).Content.ToString();

            setLeiloes(words, categ, sortText);

            SearchGrid.Visibility = Visibility.Visible;
            InspectGrid.Visibility = Visibility.Collapsed;
        }*/
    }
}