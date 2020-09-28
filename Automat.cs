using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fahrkartenautomat
{
    class Automat
    {
        private readonly Label LblBalance;
        private readonly Label LblRemainingCosts;
        private readonly Label Lbl10ct;
        private readonly Label Lbl50ct;
        private readonly Label Lbl1eu;
        private readonly Label Lbl2eu;
        private readonly Label Lbl5eu;
        private readonly Label Lbl10eu;
        private readonly Label Lbl20eu;

        private float _balance;
        public float Balance
        {
            get { return _balance; }

            set
            {
                _balance = (float)Math.Round(value, 2);
                if (_balance < 0)
                    _balance = 0;

                LblBalance.Content = _balance.ToString("0.00") + "€";
            }
        }

        private float _remainingCosts;
        private float RemainingCosts
        {
            get { return _remainingCosts; }
            set
            {
                _remainingCosts = (float)Math.Round(value, 2);
                LblRemainingCosts.Content = value.ToString("0.00") + "€";
            }
        }

        private readonly Dictionary<float, MoneyItem> _money;

        private string ticketOutput;

        public Automat(Label LblBalance, Label LblRemainingCosts, Label Lbl10ct, Label Lbl50ct, Label Lbl1eu, Label Lbl2eu, Label Lbl5eu, Label Lbl10eu, Label Lbl20eu)
        {
            this.LblBalance = LblBalance;
            this.LblRemainingCosts = LblRemainingCosts;
            this.Lbl10ct = Lbl10ct;
            this.Lbl50ct = Lbl50ct;
            this.Lbl1eu = Lbl1eu;
            this.Lbl2eu = Lbl2eu;
            this.Lbl5eu = Lbl5eu;
            this.Lbl10eu = Lbl10eu;
            this.Lbl20eu = Lbl20eu;

            _money = new Dictionary<float, MoneyItem>()
            {
                { 0.1f, new MoneyItem(0.1f, this.Lbl10ct) },
                { 0.5f, new MoneyItem(0.5f, this.Lbl50ct) },
                { 1.0f, new MoneyItem(1.0f, this.Lbl1eu) },
                { 2.0f, new MoneyItem(2.0f, this.Lbl2eu) },
                { 5.0f, new MoneyItem(5.0f, this.Lbl5eu) },
                { 10.0f, new MoneyItem(10.0f, this.Lbl10eu) },
                { 20.0f, new MoneyItem(20.0f, this.Lbl20eu) }
            };
        }

        public void BuyTicketAB()
        {
            float cost = 2.90f;
            RemainingCosts += cost;
            this.ticketOutput += $"Ticket AB \t{cost.ToString("0.00")}€\n";
        }

        public void BuyTicketBC()
        {
            float cost = 3.30f;
            RemainingCosts += cost;
            this.ticketOutput += $"Ticket BC \t{cost.ToString("0.00")}€\n";
        }

        public void BuyTicketABC()
        {
            float cost = 3.60f;
            RemainingCosts += cost;
            this.ticketOutput += $"Ticket ABC\t{cost.ToString("0.00")}€\n";
        }

        public void AddCash(float value)
        {
            _money[value].Amount += 1;
            CalculateBalance();
        }

        private void CalculateBalance()
        {
            float new_balance = 0;
            foreach (KeyValuePair<float, MoneyItem> entry in _money)
            {
                new_balance += entry.Value.Amount * entry.Key;
            }

            Balance = new_balance;
        }

        public void InsertCash(float value)
        {
            if (RemainingCosts <= 0)
            {
                MessageBox.Show("No remaining costs, buy a ticket below.");
                return;
            }

            if (Balance <= 0)
            {
                MessageBox.Show("Out of cash.");
                return;
            }

            if (_money[value].Amount > 0)
            {
                RemainingCosts -= value;
                _money[value].Amount -= 1;
                CalculateBalance();

                // Check if Tickets are paid off and calculate exchange
                if (RemainingCosts <= 0)
                {
                    float returnMoney = (float)Math.Round(Math.Abs(RemainingCosts), 2);
                    var exchange = returnMoney;

                    foreach (KeyValuePair<float, MoneyItem> entry in _money.Reverse())
                    {
                        returnMoney = (float)Math.Round(returnMoney, 2);
                        float rest = returnMoney / entry.Key;
                        if (rest >= 1)
                        {
                            returnMoney -= (float)Math.Floor(rest) * entry.Key;
                            _money[entry.Key].Amount += (int)Math.Floor(rest);
                        }
                    }

                    RemainingCosts = 0;
                    CalculateBalance();

                    // Output
                    string output = "";
                    output += "Ticket(s) purchased!\n";
                    output += "--------------------\n";
                    output += $"{this.ticketOutput}\n";
                    output += $"Exchange: \t{exchange.ToString("0.00")}€";

                    MessageBox.Show(output);
                    this.ticketOutput = "";
                }
            }
        }
    }
}
