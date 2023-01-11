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

namespace lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AWMbegin dataContext;
        public MainWindow()
        {
            InitializeComponent();
            dataContext = new AWMbegin();

        }

        private void ClientButton_Click(object sender, RoutedEventArgs e)
        {
            Window clientWindow = new ClientWindow();
            clientWindow.DataContext = dataContext;
            clientWindow.Show();
            clientWindow.Owner = this;
            ClientButton.IsEnabled = false;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Text == "123")
            {
                Window operatorWindow = new OperatorWindow();
                operatorWindow.Show();
                operatorWindow.Owner = this;
                OperatorButton.IsEnabled = false;
                operatorWindow.DataContext = dataContext;
            }
            else MessageBox.Show("Неверный пароль!");
        }
    }
}
