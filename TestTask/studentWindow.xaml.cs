using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for studentWindow.xaml
    /// </summary>
    public partial class studentWindow : Window
    {

        public Student student { get; private set; }

        public studentWindow(Student s)
        {
            InitializeComponent();
            student = s;
            this.DataContext = student;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirstName.Text != "")
            {
                if (tbLast.Text != "")
                {
                    if (rbFemale.IsChecked == true || rbMale.IsChecked == true)
                    {
                        Regex rgx = new Regex(@"[0-9]");
                        if(rgx.IsMatch(tbAge.Text))
                        {
                            if (int.Parse(tbAge.Text) < 16 || int.Parse(tbAge.Text) > 100)
                                MessageBox.Show("Возраст не может быть отрицательным и должен находиться в диапазоне [16, 100].", "Данные", MessageBoxButton.OK, MessageBoxImage.Error);
                            else
                                this.DialogResult = true;
                        }
                        else
                            MessageBox.Show("Возраст не может быть отрицательным и должен находиться в диапазоне [16, 100].", "Данные", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Укажите свой пол.", "Заполните данные", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Заполните поле: Фамилия.", "Заполните данные", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Заполните поле: Имя.", "Заполните данные", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
