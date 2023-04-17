using Zoo.ViewModel;
using Zoo.ZooShopDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для DataAddDialog.xaml
    /// </summary>
    public partial class DataAddDialog : Window
    {
        private Product _product;
        public DataAddDialog(Product product)
        {
            InitializeComponent();

            foreach(var item in App.Current.Windows)
            {
                if (item is DataGridProduct)
                {
                    this.Owner = item as Window;
                }
            }
           
            if (product is null)
            {
               _product = product = new Product();
            }
            else
            {
                _product = product;
            }
            this.DataContext = _product;
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AnimalShopEntities())
            {
                try
                {
                  
                        
                        db.Product.AddOrUpdate(_product);
                        db.SaveChanges();
                        MessageBox.Show("данные успешно сохранены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Owner as DataGridProduct)?.refres();

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private StringBuilder ValidateEntities()
        {
            var result = new StringBuilder();
            if (_product != null)
            {
                if (string.IsNullOrEmpty(_product.ProductName))
                {
                    result.AppendLine("поле названия не может быть пустым");
                }
                if (string.IsNullOrEmpty(_product.ProductDescription))
                {
                    result.AppendLine("поле описания не может быть пустым");
                }
                if (string.IsNullOrEmpty(_product.ProductCategory))
                {
                    result.AppendLine("поле категории не может быть пустым");
                }
                if (string.IsNullOrEmpty(_product.ProductManufacturer))
                {
                    result.AppendLine("поле производителя не может быть пустым");
                }
                if (_product.ProductCost <= 0)
                {
                    result.AppendLine("поле количества не может быть пустым");
                }
                if (_product.ProductDiscountAmount <= 0)
                {
                    result.AppendLine("поле величины скидки не может быть пустым");
                }
                if (_product.ProductQuantityInStock <= 0)
                {
                    result.AppendLine("поле количества на складе не может быть пустым");
                }
                if (string.IsNullOrEmpty(_product.ProductArticleNumber))
                {
                    result.AppendLine("поле артикля не может быть пустым");
                }
                
                if (string.IsNullOrEmpty(_product.ProductStatus))
                {
                    result.AppendLine("поле статуса не может быть пустым");
                }
            }
            return result;
        }
    }
}
