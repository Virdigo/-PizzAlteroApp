using PizzAlteroApp.Resourses.DataBase;
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
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzAlteroApp.Resourses.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для ChartPage.xaml
    /// </summary>
    public partial class ChartPage : Page
    {
        private PizzAltero_DataBaseEntities _context = new PizzAltero_DataBaseEntities();
        public ChartPage()
        {
            InitializeComponent();
            ChartPayments.ChartAreas.Add(new ChartArea("Main"));

            var currentSeries = new Series("Price")
            {
                IsValueShownAsLabel = true
            };
            ChartPayments.Series.Add(currentSeries);
            ComboCharts.ItemsSource = Enum.GetValues(typeof(SeriesChartType));
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingInPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPage());
        }

        private void UpdateChart(object sender, SelectionChangedEventArgs e)
        {
            if (ComboCharts.SelectedItem is SeriesChartType chartType)
            {
                Series currentSeries = ChartPayments.Series.FirstOrDefault();
                currentSeries.ChartType = chartType;
                currentSeries.Points.Clear();

                var categoriesList = _context.Product.ToList();
                foreach (var category in categoriesList)
                {
                    currentSeries.Points.AddXY(category.ProductName,
                        category.Price);
                }
            }
        }
        private void ProductPageLoaded(object sender, RoutedEventArgs e)
        {
            ID_TEXT.Text = App.currentUser.id_user.ToString();
            NAME_TEXT.Text = App.currentUser.UserName;
        }
    }
}
