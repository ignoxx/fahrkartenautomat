using System;
using System.Windows;
using System.Windows.Controls;

namespace Fahrkartenautomat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Money
        {
            ct10,
            ct50,
            eu1,
            eu2,
            eu5
        }

        private float _balance;
        private float _remainingCosts;
        private float[] _money;
        private float Balance
        {

            get { return _balance; }

            set
            {
                _balance = value; 
                LblBalance.Content = value.ToString() + "€";
            }
        }
        private float RemainingCosts { get { return _remainingCosts; } set { _remainingCosts = value; LblRemainingCosts.Content = value.ToString() + "€"; } }


        public MainWindow()
        {
            InitializeComponent();
 
        }

        private void BtnBuyTicketA_Click(object sender, RoutedEventArgs e)
        {
            float cost = 1.00f;
            RemainingCosts += cost;
        }

        private void BtnBuyTicketAB_Click(object sender, RoutedEventArgs e)
        {
            float cost = 2.50f;
            RemainingCosts += cost;
        }

        private void BtnBuyTicketABC_Click(object sender, RoutedEventArgs e)
        {
            float cost = 4.50f;
            RemainingCosts += cost;
        }

        private void Btn10ct_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 0.1f;
            Balance += value;
        }

        private void Btn50ct_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 0.5f;
            Balance += value;
        }

        private void Btn1eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 1.0f;
            Balance += value;
        }

        private void Btn2eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 2.0f;
            Balance += value;
        }

        private void Btn5eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 5.0f;
            Balance += value;
        }

        private void Btn10ct_Click(object sender, RoutedEventArgs e)
        {
            float value = 0.1f;
            onPaid(value);
        }

        private void Btn50ct_Click(object sender, RoutedEventArgs e)
        {
            float value = 0.5f;
            onPaid(value);
        }

        private void Btn1eu_Click(object sender, RoutedEventArgs e)
        {
            float value = 1.0f;
            onPaid(value);
        }

        private void Btn2eu_Click(object sender, RoutedEventArgs e)
        {
            float value = 2.0f;
            onPaid(value);
        }

        private void Btn5eu_Click(object sender, RoutedEventArgs e)
        {
            float value = 5.0f;
            onPaid(value);
        }

        private void onPaid(float value)
        {
            if (Balance >= RemainingCosts && RemainingCosts > 0)
            {
                RemainingCosts -= value;
                Balance -= value;

                if (RemainingCosts <= 0)
                {
                    Balance += Math.Abs(RemainingCosts);
                    RemainingCosts = 0;
                    MessageBox.Show("Ticket purchased!");
                }
            }
            else
            {
                MessageBox.Show("No Ticket selected or not enough cash!");
            }
            
        }

        private void BtnClosePopup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
