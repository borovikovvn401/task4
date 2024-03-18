using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using task4.Forms;
using task4.Model;

namespace task4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool isAdmin = false;

        public MainWindow()
        {
            InitializeComponent();

            cbSort.Items.Add("По алфавиту (а-я)");
            cbSort.Items.Add("По алфавиту (я-а)");
            cbSort.Items.Add("По убыванию цены");
            cbSort.Items.Add("По возрастанию цены");

            cbFiltr.Items.Add("Все");
            cbFiltr.Items.Add("0-5%");
            cbFiltr.Items.Add("5-15%");
            cbFiltr.Items.Add("15-30%");
            cbFiltr.Items.Add("30-70%");
            cbFiltr.Items.Add("70-100%");

            cbSort.SelectedIndex = 0;
            cbFiltr.SelectedIndex = 0;

        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAdmin)
            {
                setAdmin(false);
            }
            else
            {
                Window1 s = new Window1(this);
                s.Show();
                IsEnabled = false;
            }
        }

        public void setAdmin(bool i)
        {
            if (i)
            {
                isAdmin = true;
                deleteButton.Visibility = Visibility.Visible;
                editButton.Visibility = Visibility.Visible;
                addButton.Visibility = Visibility.Visible;
                recordButton.Visibility = Visibility.Visible;
                recordListButton.Visibility = Visibility.Visible;
                adminButton.Content = "Режим администратора ВКЛ";
            }
            else
            {
                isAdmin = false;
                deleteButton.Visibility = Visibility.Hidden;
                editButton.Visibility = Visibility.Hidden;
                addButton.Visibility = Visibility.Hidden;
                recordButton.Visibility = Visibility.Hidden;
                recordListButton.Visibility = Visibility.Hidden;
                adminButton.Content = "Режим администратора ВЫКЛ";
            }
        }

        public void update()
        {

            IEnumerable<Model.Service> services = EfModel.init().Services
                .Where(p => p.Title.Contains(tbSearch.Text) || p.Description.Contains(tbSearch.Text));

            switch (cbSort.SelectedIndex)
            {
                case 0:
                    services = services.OrderBy(p => p.Title);
                    break;
                case 1:
                    services = services.OrderByDescending(p => p.Title);
                    break;
                case 2:
                    services = services.OrderBy(p => p.Cost);
                    break;
                case 3:
                    services = services.OrderByDescending(p => p.Cost);
                    break;
            }

            switch (cbFiltr.SelectedIndex)
            {
                case 1:
                    services = services.Where(p => p.Discount >= 0 && p.Discount < 5);
                    break;
                case 2:
                    services = services.Where(p => p.Discount >= 5 && p.Discount < 15);
                    break;
                case 3:
                    services = services.Where(p => p.Discount >= 15 && p.Discount < 30);
                    break;
                case 4:
                    services = services.Where(p => p.Discount >= 30 && p.Discount < 70);
                    break;
                case 5:
                    services = services.Where(p => p.Discount >= 70 && p.Discount < 100);
                    break;
            }


            lvServices.ItemsSource = services.ToList();


            tbRecords.Text = services.ToList().Count + "/" + EfModel.init().Services.ToList().Count + " записей";

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void cbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addForm add = new addForm(new Service(), this);
            add.Show();
            this.IsEnabled = false;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvServices.SelectedItems.Count == 1)
            {
                Service service = lvServices.SelectedItem as Service;
                addForm add = new addForm(service, this);
                add.Show();
                IsEnabled = false;
                update();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvServices.SelectedItems.Count == 1)
            {
                Service service = lvServices.SelectedItem as Service;
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить запись?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    EfModel.init().Services.Remove(service);
                    EfModel.save();
                    update();
                }
            }
        }

        private void recordButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvServices.SelectedItems.Count == 1)
            {
                Service service = lvServices.SelectedItem as Service;
                addRecordForm addRecord = new addRecordForm(service, this);
                addRecord.Show();
                this.IsEnabled = false;
            }
        }

        private void recordListButton_Click(object sender, RoutedEventArgs e)
        {
            RecordListForm recordListForm = new RecordListForm();
            recordListForm.Show();
        }
    }
}