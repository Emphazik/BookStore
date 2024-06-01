using BookStore.ApplicationData;
using BookStore.Models;
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
using System.Data.Entity;
using System.Security.Policy;

namespace BookStore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
            AppConnect.model0db = new Entities();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
        }

        public void LoadBooks()
        {
            using (var context = new BookStoreHEntities())
            {
                var books = context.Books
                                   .Include(b => b.Authors)
                                   .Include(b => b.Categories)
                                   .Include(b => b.Publishers)
                                   .ToList();
                booksListView.ItemsSource = books;
            }
        }

        private void ShowDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var book = button.DataContext;
                detailsPopup.DataContext = book;
                detailsPopup.IsOpen = true;
            }
        }

        private void ClosePopup_Click(object sender, RoutedEventArgs e)
        {
            detailsPopup.IsOpen = false;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
