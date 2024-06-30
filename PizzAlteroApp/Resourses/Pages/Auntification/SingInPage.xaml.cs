using PizzAlteroApp.Resourses.Controllers;
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

namespace PizzAlteroApp.Resourses.Pages.Auntification
{
    /// <summary>
    /// Логика взаимодействия для SingInPage.xaml
    /// </summary>
    public partial class SingInPage : Page
    {
        UserController userController = new UserController();
        public SingInPage()
        {
            InitializeComponent();
        }

        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(TBoxLogin.Text) && !String.IsNullOrEmpty(PBoxPassword.Password))
                {
                    var user = userController.SingIn(TBoxLogin.Text.Trim().ToLower(), PBoxPassword.Password.Trim().ToLower());
                    App.currentUser= user;
                    if (TBoxLogin.Text == "Admin")
                    {
                        NavigationService.Navigate(new AdminPage());
                    }
                    else
                    {
                        NavigationService.Navigate(new UserPages.ProductPage());
                    }

                }
                else
                {
                    MessageBox.Show("Не все поля заполнены!", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Пользователь с текущими данными не найден!", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SingUpPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingUpPage());
        }
    }
}
