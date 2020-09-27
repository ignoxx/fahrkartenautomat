using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Fahrkartenautomat
{
    class Automat
    {
        private Label LblBalance;
        private Label LblRemainingCosts;
        private Label Lbl10ct;
        private Label Lbl50ct;
        private Label Lbl1eu;
        private Label Lbl2eu;
        private Label Lbl5eu;
        private Label Lbl10eu;
        private Label Lbl20eu;

        private float _balance;
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

        private float _remainingCosts;
        private float RemainingCosts
        {
            get { return _remainingCosts; }
            set
            {
                _remainingCosts = value;
                LblRemainingCosts.Content = value.ToString("0.00") + "€";
            }
        }

        private Dictionary<float, MoneyItem> _money;

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
        }

        public void BuyTicketBC()
        {
            float cost = 3.30f;
            RemainingCosts += cost;
        }

        public void BuyTicketABC()
        {
            float cost = 3.60f;
            RemainingCosts += cost;
        }

        public void AddCash(float value)
        {
            _money[value].Amount += 1;
            Balance += value;
        }

        public void InsertCash(float value)
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
                    var exchange = returnMoney;
                    Balance += returnMoney;

                    foreach (KeyValuePair<float, MoneyItem> entry in _money.Reverse())
                    {
                        // do something with entry.Value or entry.Key
                        float rest = returnMoney / entry.Key;
                        if (rest >= 1)
                        {
                            returnMoney -= (float)Math.Floor(rest) * entry.Key;
                            _money[entry.Key].Amount += (int)Math.Floor(rest);
                        }
                    }


                    RemainingCosts = 0;
                    MessageBox.Show($"Ticket(s) purchased!\nExchange: {exchange.ToString("0.00")}€");

                }
            }
            else
            {
                MessageBox.Show($"No {value.ToString("0.00")}€ cash left");
            }
        }
    }
}
