using PizzAlteroApp.Resourses.DataBase;
using PizzAlteroApp.Resourses.Pages.Auntification;
using System;
using System.Collections.Generic;
using System.IO;
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
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace PizzAlteroApp.Resourses.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.ToList();
            CmbFilterProduct.ItemsSource = PizzAltero_DataBaseEntities.GetContext().ProductType.ToList();
            CmbFilterProduct.SelectedValuePath = "id_ProductType";
            CmbFilterProduct.DisplayMemberPath = "ProductType_Name";
        }

        private void ProductAddPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductAdd(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var Remove1 = DtgProductAP.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {Remove1.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PizzAltero_DataBaseEntities.GetContext().Product.RemoveRange(Remove1);
                    PizzAltero_DataBaseEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SingInPage());
        }

        private void ReAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProductAdd((sender as Button).DataContext as Product));
        }
        private void ProductPageLoaded(object sender, RoutedEventArgs e)
        {
            ID_TEXT.Text = App.currentUser.id_user.ToString();
            NAME_TEXT.Text = App.currentUser.UserName;
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
        private void CmbFilterProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string FilterProduct = CmbFilterProduct.SelectedValue.ToString();
            DtgProductAP.ItemsSource = PizzAltero_DataBaseEntities.GetContext().Product.Where(x => x.id_ProductType.ToString() == FilterProduct).ToList();
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            var ExcelApp = new Excel.Application();

            Excel.Workbook wb = ExcelApp.Workbooks.Add();

            Excel.Worksheet worksheet = ExcelApp.Worksheets.Item[1];

            int indexRows = 1;

            worksheet.Cells[2][indexRows] = "Название";
            worksheet.Cells[3][indexRows] = "Описание";
            worksheet.Cells[4][indexRows] = "Тип продукта";
            worksheet.Cells[5][indexRows] = "Цена";

            var printItems = DtgProductAP.Items;

            foreach (Product item in printItems)
            {
                worksheet.Cells[1][indexRows + 1] = indexRows;
                worksheet.Cells[2][indexRows + 1] = item.ProductName;
                worksheet.Cells[3][indexRows + 1] = item.Description;
                worksheet.Cells[4][indexRows + 1] = item.ProductType.ProductType_Name;
                worksheet.Cells[5][indexRows + 1] = item.Price;

                indexRows++;
            }
            Excel.Range range = worksheet.Range[worksheet.Cells[2][indexRows + 1],
                    worksheet.Cells[5][indexRows + 1]];

            range.ColumnWidth = 20;

            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            ExcelApp.Visible = true;
        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            var ProductInWord = PizzAltero_DataBaseEntities.GetContext().Product.ToList();

            var ProductApplication = new Word.Application();

            Word.Document document = ProductApplication.Documents.Add();

            Word.Paragraph empParagraph = document.Paragraphs.Add();
            Word.Range empRange = empParagraph.Range;
            empRange.Text = "Products";
            empRange.Font.Bold = 4;
            empRange.Font.Italic = 4;
            empRange.Font.Color = Word.WdColor.wdColorBlack;
            empRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table paymentsTable = document.Tables.Add(tableRange, ProductInWord.Count() + 1, 4);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Название";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Описание";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Тип продукта";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Цена";

            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < ProductInWord.Count(); i++)
            {
                var ProductCurrent = ProductInWord[i];

                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = ProductCurrent.ProductName;

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = ProductCurrent.Description;

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = ProductCurrent.ProductType.ProductType_Name;

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = ProductCurrent.Price.ToString();
            }

            ProductApplication.Visible = true;

            document.SaveAs2(@"C:\Users\User\OneDrive\Desktop\PizzAlteroApp\PizzAlteroApp\Resourses\Files\Docs\Products.docx");
        }

        private void PDF_Click(object sender, RoutedEventArgs e)
        {
            var ProductInPDF = PizzAltero_DataBaseEntities.GetContext().Product.ToList();

            var ProductApplicationPDF = new Word.Application();

            Word.Document document = ProductApplicationPDF.Documents.Add();

            Word.Paragraph empParagraph = document.Paragraphs.Add();
            Word.Range empRange = empParagraph.Range;
            empRange.Text = "Products";
            empRange.Font.Bold = 4;
            empRange.Font.Italic = 4;
            empRange.Font.Color = Word.WdColor.wdColorBlack;
            empRange.InsertParagraphAfter();

            Word.Paragraph tableParagraph = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraph.Range;
            Word.Table paymentsTable = document.Tables.Add(tableRange, ProductInPDF.Count() + 1, 4);
            paymentsTable.Borders.InsideLineStyle = paymentsTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            paymentsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = paymentsTable.Cell(1, 1).Range;
            cellRange.Text = "Название";
            cellRange = paymentsTable.Cell(1, 2).Range;
            cellRange.Text = "Описание";
            cellRange = paymentsTable.Cell(1, 3).Range;
            cellRange.Text = "Тип продукта";
            cellRange = paymentsTable.Cell(1, 4).Range;
            cellRange.Text = "Цена";


            paymentsTable.Rows[1].Range.Bold = 1;
            paymentsTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < ProductInPDF.Count(); i++)
            {
                var ProductCurrent = ProductInPDF[i];

                cellRange = paymentsTable.Cell(i + 2, 1).Range;
                cellRange.Text = ProductCurrent.ProductName;

                cellRange = paymentsTable.Cell(i + 2, 2).Range;
                cellRange.Text = ProductCurrent.Description;

                cellRange = paymentsTable.Cell(i + 2, 3).Range;
                cellRange.Text = ProductCurrent.ProductType.ProductType_Name;

                cellRange = paymentsTable.Cell(i + 2, 4).Range;
                cellRange.Text = ProductCurrent.Price.ToString();
            }

            ProductApplicationPDF.Visible = true;

            document.SaveAs2(@"C:\Users\User\OneDrive\Desktop\PizzAlteroApp\PizzAlteroApp\Resourses\Files\Docs\Products.pdf", Word.WdExportFormat.wdExportFormatPDF);
        }

        private void Printer_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ChartPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChartPage());
        }

        
    }
}
