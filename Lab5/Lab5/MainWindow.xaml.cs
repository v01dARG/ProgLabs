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
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace Lab5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static BookCatalog Catalog = new BookCatalog();//Объект Книжный каталог
        public static List<FoundBook> FoundBooks = new List<FoundBook>();//найденые книги
        public MainWindow()
        {
            InitializeComponent();
            Catalog.LoadXML();
            Catalog.SelectAllBooks(ref FoundBooks);
            grid1.ItemsSource = FoundBooks;
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form_add = new Window1();
            DataContext = null;
            form_add.Owner = this;
            form_add.ShowDialog();
            if (DataContext != null)
            {
                Catalog.AddBook(((formdata)DataContext).Title, ((formdata)DataContext).Author, ((formdata)DataContext).Description, ((formdata)DataContext).ISBN);
                Catalog.SelectAllBooks(ref FoundBooks);
                grid1.Items.Refresh();
            }
        }
        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            var form_filter = new Window2();
            DataContext = null;
            form_filter.Owner = this;
            form_filter.ShowDialog();
            if (DataContext != null)
            {
                switch (((formdatafilter)DataContext).sel)
                {
                    case 0:
                        Catalog.SelectAllBooks(ref FoundBooks);
                        grid1.Items.Refresh();
                        break;
                    case 1:
                        Catalog.SelectBooksByTitle(((formdatafilter)DataContext).str, ref FoundBooks);
                        grid1.Items.Refresh();
                        break;
                    case 2:
                        Catalog.SelectBooksByPartOfTitle(((formdatafilter)DataContext).str, ref FoundBooks);
                        grid1.Items.Refresh();
                        break;
                    case 3:
                        Catalog.SelectBooksByAuthor(((formdatafilter)DataContext).str, ref FoundBooks);
                        grid1.Items.Refresh();
                        break;
                    case 4:
                        Catalog.SelectBooksByKeyWords(((formdatafilter)DataContext).str, ref FoundBooks);
                        grid1.Items.Refresh();
                        break;

                }
            }

        }
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Catalog.SaveXML();
            Application.Current.Shutdown();
        }
    }
}
