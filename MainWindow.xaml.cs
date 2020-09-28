using System.Windows;

namespace Fahrkartenautomat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Automat automat;
        public MainWindow()
        {
            InitializeComponent();
            automat = new Automat(
                LblBalance,
                LblRemainingCosts,
                Lbl10ct,
                Lbl50ct,
                Lbl1eu,
                Lbl2eu,
                Lbl5eu,
                Lbl10eu,
                Lbl20eu
             );
        }

        private void BtnBuyTicketA_Click(object sender, RoutedEventArgs e) => automat.BuyTicketAB();

        private void BtnBuyTicketAB_Click(object sender, RoutedEventArgs e) => automat.BuyTicketBC();

        private void BtnBuyTicketABC_Click(object sender, RoutedEventArgs e) => automat.BuyTicketABC();

        private void Btn10ct_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(.1f);

        private void Btn50ct_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(.5f);

        private void Btn1eu_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(1.0f);

        private void Btn2eu_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(2.0f);

        private void Btn5eu_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(5.0f);

        private void Btn10eu_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(10.0f);

        private void Btn20eu_Add_Click(object sender, RoutedEventArgs e) => automat.AddCash(20.0f);

        private void Btn10ct_Click(object sender, RoutedEventArgs e) => automat.InsertCash(.1f);

        private void Btn50ct_Click(object sender, RoutedEventArgs e) => automat.InsertCash(.5f);

        private void Btn1eu_Click(object sender, RoutedEventArgs e) => automat.InsertCash(1.0f);

        private void Btn2eu_Click(object sender, RoutedEventArgs e) => automat.InsertCash(2.0f);

        private void Btn5eu_Click(object sender, RoutedEventArgs e) => automat.InsertCash(5.0f);

        private void Btn10eu_Click(object sender, RoutedEventArgs e) => automat.InsertCash(10.0f);

        private void Btn20eu_Click(object sender, RoutedEventArgs e) => automat.InsertCash(20.0f);
    }
}
