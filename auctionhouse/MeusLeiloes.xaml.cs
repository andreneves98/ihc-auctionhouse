using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace auctionhouse
{
    public partial class MeusLeiloes : UserControl
    {
        private AuctionHouse ahref;
        private Leilao current_insp_leilao;
        private String username;

        public MeusLeiloes(String username)
        {
            InitializeComponent();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            ahref = mainWindow.ah;

            Categ.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Search_Options_Changed));
            SortPrice.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Search_Options_Changed));
            Categoria.AddHandler(ComboBox.SelectionChangedEvent, new RoutedEventHandler(Search_Options_Changed));
            this.username = username;
            setLeiloes("", "Todos", "Preço asce.");
        }

        public void Inspect_Button_Click(Leilao insp_leilao)
        {
            current_insp_leilao = insp_leilao;

            init_inspect_fields();

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
            Inspect_lei_hist.Text = "";
            int size = (current_insp_leilao.Licitacoes).Count;
            if (size > 5)
            {
                for (int i = size - 1; i > size - 6; i--)
                {
                    Inspect_lei_hist.Text += (current_insp_leilao.Licitacoes[i].user + ": " + current_insp_leilao.Licitacoes[i].value + "€\n");
                }
            }
            else
            {
                for (int i = size - 1; i > -1; i--)
                {
                    Inspect_lei_hist.Text += (current_insp_leilao.Licitacoes[i].user + ": " + current_insp_leilao.Licitacoes[i].value + "€\n");
                }
            }
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
            AddGrid.Visibility = Visibility.Collapsed;
        }

        private void Search_Options_Changed(object sender, RoutedEventArgs e)
        {
            string words = SearchWords.Text;
            string categ = ((ComboBoxItem)Categ.SelectedItem).Content.ToString();
            string sortText = ((ComboBoxItem)SortPrice.SelectedItem).Content.ToString();

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

            Leilao[] leiloes = ahref.getLeiloes(words, categ, sortby, username);
            foreach (Leilao lei in leiloes)
            {
                Leiloes_StackPanel.Children.Add(new Leiloes_leilao(this, lei));
            }
        }

        private void add_leilao(object sender, RoutedEventArgs e)
        {
            SearchGrid.Visibility = Visibility.Collapsed;
            AddGrid.Visibility = Visibility.Visible;
        }

        private void add_leilao_confirm(object sender, RoutedEventArgs e)
        {
            String category = ((ComboBoxItem)Categ.SelectedItem).Content.ToString();
            double value = 0;
            String desc = "";
            String nome = "";
            bool res = double.TryParse(TextBox_value.Text, out value);
            if (TextBox_name.Text.Length < 1)
            {
                /*Licitar_error_text.Text = "Insira um valor superior à ultima licitação.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;*/
            }else if (TextBox_desc.Text.Length < 1)
            {
                /*Licitar_error_text.Text = "Insira um valor superior à ultima licitação.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;*/
            }else if (!res)
            {
                /*Licitar_error_text.Text = "Insira um valor numérico.";
                // display error
                LicitarSuccess.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarError.Visibility = Visibility.Visible;*/
            }else
            {
                /*// clean prev error
                LicitarError.Visibility = Visibility.Collapsed;
                LicitarSuccess.Visibility = Visibility.Collapsed;

                // display success
                LicitarSuccess.Visibility = Visibility.Visible;

                // clear value
                licitar_text.Text = "";*/

                nome = TextBox_name.Text;
                desc = TextBox_desc.Text;

                TextBox_name.Text = "";
                TextBox_desc.Text = "";
                TextBox_value.Text = "";

                Leilao l = l = new Leilao(nome, desc, "Aberto", category, DateTime.Now.AddDays(2), "images/maquina.jpg", username);
                ahref.addLeilao(l);

                SearchGrid.Visibility = Visibility.Visible;
                AddGrid.Visibility = Visibility.Collapsed;

                setLeiloes("", "Todos", "Preço asce.");

            }
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_name_hint.Visibility = Visibility.Visible;
            if (TextBox_name.Text.Length > 0)
            {
                TextBox_name_hint.Visibility = Visibility.Hidden;
            }
        }

        private void desc_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_desc_hint.Visibility = Visibility.Visible;
            if (TextBox_desc.Text.Length > 0)
            {
                TextBox_desc_hint.Visibility = Visibility.Hidden;
            }
        }

        private void value_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox_value_hint.Visibility = Visibility.Visible;
            if (TextBox_value.Text.Length > 0)
            {
                TextBox_value_hint.Visibility = Visibility.Hidden;
            }
        }
    }
}