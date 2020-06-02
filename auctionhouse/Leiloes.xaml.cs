using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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

            setLeiloes("", "Todos", "Preço menor");
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

            String is_owner = "";
            if (current_insp_leilao.Owner == ahref.getUsername())
            {
                is_owner = " (Tu)";
            }

            Inspect_lei_owner.Text = current_insp_leilao.Owner + is_owner;
            LicitarOption.Visibility = Visibility.Visible;


            double rating = ahref.getRating(current_insp_leilao.Owner);
            int i = 0;
            foreach (UIElement elem in Inspect_lei_rating.Children){
                if(i < rating)
                {
                    ((MaterialDesignThemes.Wpf.PackIcon)elem).Foreground = Brushes.Gold;
                }
                else 
                {
                    ((MaterialDesignThemes.Wpf.PackIcon)elem).Foreground = Brushes.LightGray;
                }
                i++;
            }
                
            if (current_insp_leilao.Estado == "Aberto")
            {
                Inspect_lei_estado.Foreground = Brushes.Green;
                Inspect_lei_tempo.Text = "Tempo restante: " + current_insp_leilao.timeToEnd();
            }
            else // Fechado
            {
                Inspect_lei_estado.Foreground = Brushes.PaleVioletRed;
                Inspect_lei_tempo.Text = "Tempo restante: " + "0d 00:00:00h";

                // hide
                LicitarOption.Visibility = Visibility.Collapsed;
            }

            if (current_insp_leilao.hasLicitacoes())
            {
                String users_licit = "";
                if (current_insp_leilao.getCurrentValueUser() == ahref.getUsername())
                {
                    users_licit = "(Teu)";
                }
                Inspect_lei_ult_licit.Text = "Última licitação: " + current_insp_leilao.getCurrentValue().ToString() + " € " + users_licit;
            }
            else
            {
                Inspect_lei_ult_licit.Text = "Valor inicial: " + current_insp_leilao.getCurrentValue().ToString() + " €";
            }
            
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
            if (sortText == "Preço maior")
            {
                sortby = -1;
            }

            Leiloes_StackPanel.Children.Clear();

            Leilao[] leiloes = ahref.getLeiloes(words, categ, sortby, null);
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
            if (current_insp_leilao.Owner == ahref.getUsername())
            {
                Licitar_error_text.Text = "Não pode licitar no seu leilão.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;
            }
            else if (!res)
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
            else if (value - current_insp_leilao.getCurrentValue() < 1)
            {
                Licitar_error_text.Text = "A licitação tem que ser superior por pelo menos 1€";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBoxResult m_res = MessageBox.Show("Confirmar Licitação de "+ value+"€ ?", "Confirmar",MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(m_res == MessageBoxResult.Yes)
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
                    init_inspect_fields();
                }
            }
        }
    }
}