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
using Microsoft.EntityFrameworkCore;
using task4.Model;

namespace task4.Forms
{
    /// <summary>
    /// Логика взаимодействия для RecordListForm.xaml
    /// </summary>
    public partial class RecordListForm : Window
    {
        public RecordListForm()
        {
            InitializeComponent();

            dg.ItemsSource = EfModel.init().ClientServices.Include(p => p.Client).Include(p => p.Service)
                .Where(p => p.StartTime == DateTime.Today || p.StartTime == DateTime.Today.AddDays(1))
                .ToList();
        }
    }
}
