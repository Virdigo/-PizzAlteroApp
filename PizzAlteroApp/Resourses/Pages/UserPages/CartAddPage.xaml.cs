using PizzAlteroApp.Resourses.DataBase;
using PizzAlteroApp.Resourses.Pages.AdminPages;
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

namespace PizzAlteroApp.Resourses.Pages.UserPages
{
    /// <summary>
    /// Логика взаимодействия для CartAddPage.xaml
    /// </summary>
    public partial class CartAddPage : Page
    {
        private Cart _currentCart = new Cart();
        public CartAddPage()
        {
            InitializeComponent();
            DataContext = _currentCart;
            CmbSelectProduct.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.ToList();
            CmbSelectProduct.SelectedValuePath = "id_Product";
            CmbSelectProduct.DisplayMemberPath = "ProductName";
            CmbSelectUser.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Users.ToList();
            CmbSelectUser.SelectedValuePath = "id_user";
            CmbSelectUser.DisplayMemberPath = "Login";

        }

        private void AddCartProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentCart.id_user != App.currentUser.id_user)
                errors.AppendLine("Выбирите свой аккаунт");
            if (_currentCart.id_Product == null)
                errors.AppendLine("Выберите продукт");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentCart.id_Cart == 0)
                PizzAltero_DataBaseEntities.GetContext().Cart.Add(_currentCart);
            try
            {
                PizzAltero_DataBaseEntities.GetContext().SaveChanges();
                MessageBox.Show("Продукт добавлен в корзину!");
                NavigationService.Navigate(new ProductPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
