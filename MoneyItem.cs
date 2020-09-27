using System;
using System.Collections.Generic;
using System.Text;

namespace Fahrkartenautomat
{
    class MoneyItem
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
}
