using PizzAlteroApp.Resourses.Controllers;
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
using PizzAlteroApp.Resourses.DataBase;

namespace PizzAlteroApp.Resourses.Pages.Auntification
{
    /// <summary>
    /// Логика взаимодействия для SingUpPage.xaml
    /// </summary>
    public partial class SingUpPage : Page
    {
        UserController userController = new UserController();
        public SingUpPage()
        {
            InitializeComponent();
        }

        private void SingInPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingInPage());
        }

        private void SingUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(TBoxLogin.Text) &&
                    !String.IsNullOrEmpty(PBoxPassword.Password) &&
                    !String.IsNullOrEmpty(TBoxUserName.Text) &&
                    !String.IsNullOrEmpty(TBoxMail.Text) &&
                    !String.IsNullOrEmpty(TBoxAddress.Text) &&
                    !String.IsNullOrEmpty(TBoxCardNumber.Text))
                {
                    var user = userController.CreateNewUser(
                        TBoxUserName.Text,
                        TBoxAddress.Text,
                        TBoxCardNumber.Text,
                        PBoxPassword.Password,
                        TBoxMail.Text,
                        TBoxLogin.Text);
                    App.currentUser = user;
                    NavigationService.Navigate(new UserPages.ProductPage());
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Системная ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
