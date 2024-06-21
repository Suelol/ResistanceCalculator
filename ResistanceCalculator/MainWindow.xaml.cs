using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ResistanceCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void CalculateResistance(object sender, RoutedEventArgs e)
        {
            double resistance1 = GetResistanceValue(textBoxResistance1);
            double resistance2 = GetResistanceValue(textBoxResistance2);

            if (resistance1 == 0)
            {
                textBlockResult.Text = "Ошибка. Введите ненулевое значение для сопротивления 1.";
                return;
            }

            if (resistance2 == 0)
            {
                textBlockResult.Text = "Ошибка. Введите ненулевое значение для сопротивления 2.";
                return;
            }

            if (GetResistanceText(textBoxResistance1).Contains(" "))
            {
                MessageBox.Show("Ошибка: между цифрами не должно быть пробелов");
                return;
            }
            if (GetResistanceText(textBoxResistance1).Contains(" "))
            {
                MessageBox.Show("Ошибка: между цифрами не должно быть пробелов");
                return;
            }

            double totalResistance = 0;

            if (radioButtonSeries.IsChecked == true)
            {
                // Вычисление общего сопротивления для последовательного соединения
                totalResistance = resistance1 + resistance2;
            }
            else if (radioButtonParallel.IsChecked == true)
            {
                // Вычисление общего сопротивления для параллельного соединения
                totalResistance = (resistance1 * resistance2) / (resistance1 + resistance2);
            }

            if (totalResistance > 1000)
            {
                totalResistance /= 1000; // Перевод в килоомы
                textBlockResult.Text = $"Сопротивление цепи: {totalResistance} кОм";
            }
            else
            {
                textBlockResult.Text = $"Сопротивление цепи: {totalResistance} Ом";
            }
        }

        private void ClearFields(object sender, RoutedEventArgs e)
        {
            textBoxResistance1.Text = "0";
            textBoxResistance2.Text = "0";
            textBlockResult.Text = "";
        }

        private double GetResistanceValue(TextBox textBox)
        {
            string resistanceText = textBox.Text;
            resistanceText = RemoveInvalidChars(resistanceText);

            if (string.IsNullOrWhiteSpace(resistanceText))
            {
                return 0;
            }

            double resistance;
            return double.TryParse(resistanceText, out resistance) ? resistance : 0;
        }

        private string GetResistanceText(TextBox textBox)
        {
            string resistanceText = textBox.Text;
            return resistanceText;
        }

        private string RemoveInvalidChars(string input)
        {
            return Regex.Replace(input, "[^0-9.,-]", "");
        }
    }
           
    }
