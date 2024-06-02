using BookStore.ApplicationData;
using BookStore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;

namespace BookStore
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Books> Books { get; set; }
        private ObservableCollection<Books> FilteredBooks { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            LoadBooks();
            AppConnect.model0db = new Entities();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
        }

        private void LoadBooks()
        {
            using (var context = new BookStoreHEntities())
            {
                var books = context.Books
                                   .Include(b => b.Authors)
                                   .Include(b => b.Categories)
                                   .Include(b => b.Publishers)
                                   .ToList();
                Books = new ObservableCollection<Books>(books);
                FilteredBooks = new ObservableCollection<Books>(Books);
                booksListView.ItemsSource = FilteredBooks;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            var filtered = Books.Where(b => b.Title.ToLower().Contains(searchText)).ToList();
            FilteredBooks.Clear();
            foreach (var book in filtered)
            {
                FilteredBooks.Add(book);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            searchTextBox.Clear();
            FilteredBooks.Clear();
            foreach (var book in Books)
            {
                FilteredBooks.Add(book);
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

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string sortOption = selectedItem.Content.ToString();
                SortBooks(sortOption);
            }
        }

        private void SortBooks(string sortOption)
        {
            switch (sortOption)
            {
                case "Сортировать по названию ↑":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderBy(b => b.Title));
                    break;
                case "Сортировать по названию ↓":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderByDescending(b => b.Title));
                    break;
                case "Сортировать по автору ↑":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderBy(b => b.Authors.Name));
                    break;
                case "Сортировать по автору ↓":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderByDescending(b => b.Authors.Name));
                    break;
                case "Сортировать по цене ↑":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderBy(b => b.Price));
                    break;
                case "Сортировать по цене ↓":
                    FilteredBooks = new ObservableCollection<Books>(FilteredBooks.OrderByDescending(b => b.Price));
                    break;
            }
            booksListView.ItemsSource = FilteredBooks;
        }
    }
}
