using System.Windows;
using System.Windows.Controls;

namespace auctionhouse
{
    public partial class Leiloes : UserControl
    {
        private AuctionHouse ahref;
        private Leilao current_insp_leilao;

        public Leiloes()
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;

            Categ.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Search_Options_Changed));
            SortPrice.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Search_Options_Changed));

            setLeiloes("", "Todos", "Preço asce.");
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
        }

        public void Inspect_Back_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchGrid.Visibility = Visibility.Visible;
            InspectGrid.Visibility = Visibility.Collapsed;
        }

        private void Search_Options_Changed(object sender, RoutedEventArgs e)
        {
            string words = SearchWords.Text;
            string categ = ((ComboBoxItem) Categ.SelectedItem).Content.ToString();
            string sortText = ((ComboBoxItem) SortPrice.SelectedItem).Content.ToString();

            setLeiloes(words, categ, sortText);
        }

        private void setLeiloes(string words, string categ, string sortText)
        {
            int sortby = 1;
            if (sortText == "Preço desc.")
            {
                sortby = -1;
            }

            Leiloes_StackPanel.Children.Clear();

            Leilao[] leiloes = ahref.getLeiloes(words, categ, sortby);
            foreach (Leilao lei in leiloes)
            {
                Leiloes_StackPanel.Children.Add(new Leiloes_leilao(this, lei));
            }
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
            }else if (value <= current_insp_leilao.highestLicitacao())
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

                current_insp_leilao.addLicitacao(new Licitacao(ahref.getUsername(), value));

                init_inspect_fields();
            }
        }
    }
}