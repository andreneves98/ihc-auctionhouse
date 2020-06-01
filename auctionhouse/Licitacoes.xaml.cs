using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace auctionhouse
{
    public partial class Licitacoes : UserControl
    {
        private AuctionHouse ahref;
        private Leilao current_insp_leilao;

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
            messageGrid.Visibility = Visibility.Collapsed;

            Leilao[] userLeiloes = ahref.getUserLeiloes("Artur Filipe");
            if(userLeiloes.Length == 0)
            {
                messageGrid.Visibility = Visibility.Visible;
            }

            foreach (Leilao lei in userLeiloes)
            {
                Leiloes_StackPanel.Children.Add(new Leiloes_leilao(this, lei));
            }
        }

        public void Inspect_Button_Click(Leilao insp_leilao)
        {
            current_insp_leilao = insp_leilao;

            init_inspect_fields();

            // clean prev error
            LicitarError.Visibility = Visibility.Collapsed;
            LicitarSuccess.Visibility = Visibility.Collapsed;

            // clear value
            licitar_text.Text = "";

            InspectGrid.Visibility = Visibility.Visible;
            contentGrid.Visibility = Visibility.Collapsed;
        }

        public void init_inspect_fields()
        {
            // init fields
            Inspect_lei_nome.Text = current_insp_leilao.Nome;
            Inspect_lei_desc.Text = current_insp_leilao.Descricao;
            Inspect_lei_estado.Text = current_insp_leilao.Estado;
            Inspect_lei_ult_licit.Text = "Última licitação: " + current_insp_leilao.getCurrentValue().ToString() + " €";
            Inspect_lei_tempo.Text = "Tempo restante: " + current_insp_leilao.timeToEnd();
            Inspect_lei_img.Source = new BitmapImage(new Uri(current_insp_leilao.imgPath, UriKind.Relative));
        }

        public void Inspect_Back_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // refresh leiloes list
            /*string words = SearchWords.Text;
            string categ = ((ComboBoxItem)Categ.SelectedItem).Content.ToString();
            string sortText = ((ComboBoxItem)SortPrice.SelectedItem).Content.ToString();

            setLeiloes(words, categ, sortText);*/

            contentGrid.Visibility = Visibility.Visible;
            InspectGrid.Visibility = Visibility.Collapsed;
        }

        private void licitar_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_licitar_hint.Visibility = Visibility.Visible;
            if (licitar_text.Text.Length > 0)
            {
                TextBox_licitar_hint.Visibility = Visibility.Hidden;
            }
        }

        private void Licitar_Button_Click(object sender, RoutedEventArgs e)
        {
            double value = 0;
            bool res = double.TryParse(licitar_text.Text, out value);
            if (!res)
            {
                Licitar_error_text.Text = "Insira um valor numérico.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;
            }
            else if (value <= current_insp_leilao.getCurrentValue())
            {
                Licitar_error_text.Text = "Insira um valor superior à ultima licitação.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;
            }
            else
            {
                // clean prev error
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarSuccess.Visibility = Visibility.Collapsed;

                // display success
                LicitarSuccess.Visibility = Visibility.Visible;

                // clear value
                licitar_text.Text = "";

                Licitacao licit = new Licitacao(ahref.getUsername(), value);
                current_insp_leilao.addLicitacao(licit); // add to Leilao
                ahref.addUser_Licitacoes(licit); // add to User's list of licitações

                init_inspect_fields();
            }
        }
    }
}