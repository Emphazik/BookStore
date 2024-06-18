using BookStore.ApplicationData;
using BookStore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using BookStore.AdminWindows;
using System;

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
            LoadCategories(); 
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

        private void LoadCategories()
        {
            using (var context = new BookStoreHEntities())
            {
                var categories = context.Categories.ToList();
                categoryComboBox.Items.Clear();
                categoryComboBox.Items.Add(new ComboBoxItem { Content = "Все категории" });
                foreach (var category in categories)
                {
                    categoryComboBox.Items.Add(new ComboBoxItem { Content = category.Name });
                }
                categoryComboBox.SelectedIndex = 0; 
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

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var book = button.DataContext as Books;
                if (book != null)
                {
                    if (MessageBox.Show($"Вы точно хотите удалить книгу '{book.Title}'?", "Подтверждение удаления",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            using (var context = new BookStoreHEntities())
                            {
                                var existingBook = context.Books.Find(book.idBook);
                                if (existingBook != null)
                                {
                                    context.Books.Remove(existingBook);
                                    context.SaveChanges();
                                    MessageBox.Show("Книга успешно удалена");
                                    LoadBooks();
                                }
                                else
                                {
                                    MessageBox.Show("Книга не найдена в базе данных");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка удаления книги: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Поиск работает по названию книги. Введите название книги в поле поиска и нажмите кнопку поиска или введите текст для автоматического обновления результатов.\n\n" +
                            "Сортировка позволяет упорядочить книги по названию, автору и цене. Выберите вариант сортировки в выпадающем списке и книги будут отсортированы в соответствии с выбранным параметром.",
                            "Информация о работе поиска и сортировки",
                            MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddBooks add = new AddBooks();
            add.Show();
            this.Close();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                Books book = button.DataContext as Books;
                if (book != null)
                {
                    EditBooks edit = new EditBooks(book);
                    edit.Show();
                    this.Close();

                    LoadBooks();
                }
            }
        }


        private void User_Click(object sender, RoutedEventArgs e)
        {
            UsersList user = new UsersList();
            user.Show();
            this.Close();
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

        private void FilterBooksByCategory()
        {
            string selectedCategory = (categoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedCategory == "Все категории")
            {
                FilteredBooks = new ObservableCollection<Books>(Books);
            }
            else
            {
                FilteredBooks = new ObservableCollection<Books>(Books.Where(b => b.Categories.Name == selectedCategory));
            }
            booksListView.ItemsSource = FilteredBooks;
        }
        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterBooksByCategory();
        }
    }
}
