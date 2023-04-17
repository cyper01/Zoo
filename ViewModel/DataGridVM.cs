using Zoo.ZooShopDb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Zoo.ViewModel
{
    public class DataGridVM : BaseVm
    {
        private Product _SelectedItem;
        private ObservableCollection<Product> _product;

        public ObservableCollection<Product> Product
        {
            get => _product;
            set
            {
                _product = value;
                OnpropertyChanged(nameof(Product));
            }
        }
        public Product SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem= value;
                OnpropertyChanged(nameof(SelectedItem));
            }
        }
        public DataGridVM()
        {
            Product = new ObservableCollection<Product>();

            LoadData();
        }

        public void LoadData()
        {
            if( Product.Count != 0)
            {
                Product.Clear();
            }

            var result = DbStorage.Db_s.Product.ToList();
            result.ForEach(elem => Product?.Add(elem));
        }
        public void DeleteItenData()
        {
            if (!(SelectedItem is null))
            {
                using (var db = new AnimalShopEntities())
                {
                    var result = MessageBox.Show("вы действительно хотите удалить этот элемент?", "предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var ItemForDelete = db.Product.Where(elem => elem.ProductArticleNumber == SelectedItem.ProductArticleNumber).FirstOrDefault();
                            db.Product.Remove(ItemForDelete);
                            db.SaveChanges();
                            LoadData();
                            MessageBox.Show("данные успешно удалены", "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
    }
}
