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
using task4.Model;

namespace task4.Forms
{
    /// <summary>
    /// Логика взаимодействия для addRecordForm.xaml
    /// </summary>
    public partial class addRecordForm : Window
    {

        Service service;
        MainWindow mainWindow;
        ClientService clientService;

        public addRecordForm(Service s, MainWindow mw)
        {
            this.service = s;
            this.mainWindow = mw;
            clientService = new ClientService();
            InitializeComponent();
            DataContext = clientService;
            serviceName.Text = service.Title;
            serviceDuration.Text = (service.DurationInSeconds / 60) + " минут.";
            this.Title = "Запись клиента";

            cbClients.ItemsSource = EfModel.init().Clients.ToList();
            clientService.Service = s;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            EfModel.init().ClientServices.Add(clientService);
            EfModel.save();
            this.Close();
            mainWindow.IsEnabled = true;
        }

        private string date ="", time = "";

        private void timePicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            time = timePicker.Text;
        }

        private void datePicker_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            date = datePicker.Text;
        }
    }
}
