using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Fahrkartenautomat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class MoneyItem
        {
            readonly System.Windows.Controls.Label _gui_label;
            private int _amount;
            public int Amount
            {
                get { return _amount; }
                set
                {
                    _amount = value;
                    if (_gui_label != null)
                        _gui_label.Content = value.ToString() + "x";


                }
            }
            public float Value { get; set; }

            public MoneyItem(float value, System.Windows.Controls.Label label)
            {
                Amount = 0;
                Value = value;
                _gui_label = label;
            }
        }

        private float _balance;
        private float _remainingCosts;
        private readonly Dictionary<float, MoneyItem> _money;

        public float Balance
        {
            get { return _balance; }

            set
            {
                _balance = value;
                if (_balance < 0)
                    _balance = 0;

                LblBalance.Content = _balance.ToString("0.00") + "€";
            }
        }
        private float RemainingCosts
        {
            get { return _remainingCosts; }
            set
            {
                _remainingCosts = value;
                LblRemainingCosts.Content = value.ToString("0.00") + "€";
            }
        }


        public MainWindow()
        {
            InitializeComponent();

            _money = new Dictionary<float, MoneyItem>()
            {
                { 0.1f, new MoneyItem(0.1f, Lbl10ct) },
                { 0.5f, new MoneyItem(0.5f, Lbl50ct) },
                { 1.0f, new MoneyItem(1.0f, Lbl1eu) },
                { 2.0f, new MoneyItem(2.0f, Lbl2eu) },
                { 5.0f, new MoneyItem(5.0f, Lbl5eu) }
            };
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
            _money[value].Amount += 1;
            Balance += value;
        }

        private void Btn50ct_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 0.5f;
            _money[value].Amount += 1;
            Balance += value;
        }

        private void Btn1eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 1.0f;
            _money[value].Amount += 1;
            Balance += value;
        }

        private void Btn2eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 2.0f;
            _money[value].Amount += 1;
            Balance += value;
        }

        private void Btn5eu_Add_Click(object sender, RoutedEventArgs e)
        {
            float value = 5.0f;
            _money[value].Amount += 1;
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
            if (RemainingCosts <= 0)
            {
                MessageBox.Show("No remaining costs, buy a ticket below");
                return;
            }

            if (Balance <= 0)
            {
                MessageBox.Show("Out of cash");
                return;
            }

            if (_money[value].Amount > 0)
            {
                RemainingCosts -= value;
                _money[value].Amount -= 1;
                Balance -= value;

                if (RemainingCosts <= 0)
                {
                    float returnMoney = Math.Abs(RemainingCosts);
                    Balance += returnMoney;


                    foreach (KeyValuePair<float, MoneyItem> entry in _money.Reverse())
                    {
                        // do something with entry.Value or entry.Key
                        float rest = returnMoney / entry.Key;
                        if (rest >= 1)
                        {
                            returnMoney -= (float) Math.Floor(rest) * entry.Key;
                            _money[entry.Key].Amount += (int) Math.Floor(rest);
                        }
                    }


                    RemainingCosts = 0;
                    MessageBox.Show("Ticket(s) purchased!\nExchange: " + returnMoney.ToString("0.00"));
                }
            }
            else
            {
                MessageBox.Show($"No {value.ToString("0.00")}€ cash left");
            }
        }
    }
}
