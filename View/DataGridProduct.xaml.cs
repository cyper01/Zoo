using Zoo.ViewModel;
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

namespace Zoo.View
{
    /// <summary>
    /// Логика взаимодействия для DataGridProduct.xaml
    /// </summary>
    public partial class DataGridProduct : Window
    {
        public DataGridProduct()
        {
            InitializeComponent();

            this.DataContext = new DataGridVM();
        }

        private void BtnAddData_Click(object sender, RoutedEventArgs e)
        {
            var AddWindows = new DataAddDialog(null);
            AddWindows.ShowDialog();
        }
        public void refres()
        {
            (DataContext as DataGridVM).LoadData();
        }
        private void BtnEditData_Click(object sender, RoutedEventArgs e)
        {
            var AddWindows = new DataAddDialog((DataContext as DataGridVM).SelectedItem);
            AddWindows.ShowDialog();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as DataGridVM).DeleteItenData();
        }
    }
}
