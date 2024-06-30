using PizzAlteroApp.Resourses.DataBase;
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

namespace PizzAlteroApp.Resourses.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Page
    {
        private Product _currentProduct = new Product();
        public ProductAdd(Product selectedProduct)
        {
            if (selectedProduct != null)
            {
                _currentProduct= selectedProduct;
            }
            InitializeComponent();
            DataContext= _currentProduct;
            CmbProductType.ItemsSource = PizzAltero_DataBaseEntities.GetContext().ProductType.ToList();
            CmbProductType.SelectedValuePath = "id_ProductType";
            CmbProductType.DisplayMemberPath = "ProductType_Name";
        }

        private void BackToAdminPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }

        private void AddNewProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentProduct.ProductName))
            {
                errors.AppendLine("Укажите имя продукта");
            }
            if (string.IsNullOrWhiteSpace(_currentProduct.Description))
                errors.AppendLine("Опишите продукт");
            if (_currentProduct.Price == null)
                errors.AppendLine("Укажите цену");
            if (_currentProduct.id_ProductType == null)
                errors.AppendLine("Укажите тип");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentProduct.id_Product == 0)
                PizzAltero_DataBaseEntities.GetContext().Product.Add(_currentProduct);
            try
            {
                PizzAltero_DataBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены");
                NavigationService.Navigate(new AdminPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CmbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
