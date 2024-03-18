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
using System.Windows.Shapes;

namespace task4.Forms
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        MainWindow mainwindow;

        public Window1(MainWindow mw)
        {
            InitializeComponent();
            this.Title = "Код доступа";
            this.ResizeMode = ResizeMode.NoResize;
            this.mainwindow = mw;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {

            if (tbCode.Text == "0000")
            {
                mainwindow.adminButton.Content = "Режим администратора ВыКЛ";
                mainwindow.setAdmin(true);
                mainwindow.IsEnabled = true;
                this.Close();

            }
            else
            {
                MessageBox.Show("Неверный код доступа");
            }
        }
    }
}
