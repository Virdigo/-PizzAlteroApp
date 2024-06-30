using PizzAlteroApp.Resourses.DataBase;
using PizzAlteroApp.Resourses.Pages.Auntification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        
        public CartPage()
        {
            InitializeComponent();
            DtgCartAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Cart.Where(x => x.id_user == App.currentUser.id_user).ToList();
          
        }
        private void ProductPageLoaded(object sender, RoutedEventArgs e)
        { 
            NAME_TEXT.Text = App.currentUser.UserName;
        }
        private void CartPage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ProductPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductPage());
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingInPage());
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            int sum = PizzAltero_DataBaseEntities.GetContext().Cart.Where(x => x.id_user == App.currentUser.id_user).Sum(x => x.Product.Price);
            DtgCartAP.SelectAll();
            var CartForRemoving = DtgCartAP.SelectedItems.Cast<Cart>().ToList();
            if (MessageBox.Show($"Подтвердить оплату?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PizzAltero_DataBaseEntities.GetContext().Cart.RemoveRange(CartForRemoving);
                    PizzAltero_DataBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Оплата проведена успешно!");
                    DtgCartAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Cart.Where(x => x.id_user == App.currentUser.id_user).ToList();

                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message.ToString());
                }

            }

            string smtpServer = "smtp.mail.ru";
            int smtpPort = 587;
            string smtpUsername = "ramzone277@mail.ru";
            string smtpPassword = "пароль от почты";

            
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(smtpUsername);
                    mailMessage.To.Add($"{App.currentUser.Mail}");
                    mailMessage.Subject = "Кассовый чек PizzAltera";
                    mailMessage.Body = $"Кассовый чек \r\n" +
                        $"Приход\r\n" +
                        $"ООО «PizzaAltera»\r\n" +
                        $"142181, V. Indipendenza 33, Bologna\r\n" +
                        $"ИНН 7721546864\r\n" +
                        $"https://www.pizzaltero.it/ \r\n"+
                        $"*******************************************\r\n" +
                        $"Итоговая стоимость заказа - {sum} рубля(ей)\r\n" +
                        $"Спасибо за покупку.\r\n" +
                        $"*******************************************\r\n" +
                        $"Налогообложение\r\n" +
                        $"ОСН\r\n" +
                        $"РН ККТ\r\n" +
                        $"0007114097011473\r\n" +
                        $"№ ФД\r\n" +
                        $"110143\r\n" +
                        $"№ ФН\r\n" +
                        $"7380440700332940\r\n" +
                        $"ФПД\r\n" +
                        $"664282089\r\n" +
                        $"Сайт ФНС\r\n" +
                        $"nalog.ru\r\n" +
                        $"Эл. адрес отправителя\r\n" +
                        $"ramzone277@mail.ru";


                    try
                    {
                        smtpClient.Send(mailMessage);
                        Console.WriteLine("Сообщение успешно отправлено.");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
                    }
                }
            }
            NavigationService.Navigate(new ProductPage());
        }
    }
}
