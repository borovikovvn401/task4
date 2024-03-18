using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using task4.Model;

namespace task4.Forms
{
    /// <summary>
    /// Логика взаимодействия для addForm.xaml
    /// </summary>
    public partial class addForm : Window
    {

        Service service1;
        MainWindow mainWindow;

        public addForm(Service service, MainWindow mw)
        {
            this.service1 = service;
            this.mainWindow = mw;
            DataContext = this.service1;
            InitializeComponent();


            if (service1.Id == 0)
            {
                this.service1 = new Service();
            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (service1.Id == 0)
                EfModel.init().Services.Add(service1);
            EfModel.save();

            if(service1.Id != 0)
                EfModel.init().Entry(service1).Reload();



            mainWindow.IsEnabled = true;
            this.Close();

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog { Filter = "AllFiles|*.*" };
            if(openFileDialog.ShowDialog() == true)
            {
                service1.MainImagePath = "Услуги школы\\" + new FileInfo(openFileDialog.FileName).Name;
            }
        }
    }
}
