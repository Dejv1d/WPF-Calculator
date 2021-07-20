using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFKalkulačka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string DecimalSeparator => System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;
        public MainWindow()
        {
            InitializeComponent();
            btnPoint.Content = DecimalSeparator;
            btnSum.Tag = new Sum();
            btnSubtraction.Tag = new Substraction();
            btnDivision.Tag = new Division();
            btnMultiplication.Tag = new Multipliation();
        }

        private void regularButtonClick(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == "0")
                txtInput.Text = "";
            
            txtInput.Text = $"{txtInput.Text}{((Button)sender).Content}";
         
        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text.Contains(this.DecimalSeparator))
                return;

            regularButtonClick(sender, e);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == "0")
                return;

            txtInput.Text = txtInput.Text.Substring(0, txtInput.Text.Length - 1);
            if (txtInput.Text == "")
                txtInput.Text = "0";
        }

        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
            => txtInput.Text = "0";
        

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            FirstValue = 0;
            CurrentOperation = null;
            txtInput.Text = "0";
        }

        public interface IOperations
        {
            decimal DoOperations(decimal val1, decimal val2);
        }

        public class Sum : IOperations
        {
            public decimal DoOperations(decimal val1, decimal val2) => val1 + val2;
        }
        public class Substraction : IOperations
        {
            public decimal DoOperations(decimal val1, decimal val2) => val1 - val2;
        }
        public class Division : IOperations
        {
            public decimal DoOperations(decimal val1, decimal val2) => val1 / val2;
        }
        public class Multipliation : IOperations
        {
            public decimal DoOperations(decimal val1, decimal val2) => val1 * val2;
        }

        IOperations CurrentOperation;

        decimal FirstValue { get; set; }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOperation == null)
                FirstValue = Convert.ToDecimal(txtInput.Text);

            CurrentOperation = (IOperations)((Button)sender).Tag;
            txtInput.Text = "";
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentOperation == null)
                return;

            if (txtInput.Text == "")
                return;

            decimal val2 = Convert.ToDecimal(txtInput.Text);
            txtInput.Text = CurrentOperation.DoOperations(FirstValue, val2).ToString();
        }
    }


}
