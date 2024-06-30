using PizzAlteroApp.Resourses.DataBase;
using PizzAlteroApp.Resourses.Pages.AdminPages;
using PizzAlteroApp.Resourses.Pages.Auntification;
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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private Cart _currentCart = new Cart();
        public ProductPage()
        {
            InitializeComponent();
            DataContext = _currentCart;
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.ToList();
            CmbFilterProduct.ItemsSource = PizzAltero_DataBaseEntities.GetContext().ProductType.ToList();
            CmbFilterProduct.SelectedValuePath = "id_ProductType";
            CmbFilterProduct.DisplayMemberPath = "ProductType_Name";
        }

        private void ProductPageLoaded(object sender, RoutedEventArgs e)
        {
            NAME_TEXT.Text = App.currentUser.UserName;
        }

        private void CartPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }


        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingInPage());
        }

        private void CmbFilterProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string FilterProduct = CmbFilterProduct.SelectedValue.ToString();
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.Where(x => x.id_ProductType.ToString() == FilterProduct).ToList();

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartAddPage());

        }

        private void TbSerch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string Search = TbSerch.Text;
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.
                Where(x => x.ProductName.Contains(Search)).ToList();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            TbSerch.Clear();
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.ToList();
        }

        private void CmbSelectProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductPage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
