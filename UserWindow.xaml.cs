using BookStore.ApplicationData;
using BookStore.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using BookStore.AdminWindows;
using System;
using System.Data;

namespace BookStore.AdminWindows
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private ObservableCollection<Books> Books { get; set; }
        private ObservableCollection<Books> FilteredBooks { get; set; }

        private Users curuser = new Users();
        public UserWindow(Users user)
        {
            InitializeComponent();
            LoadBooks();
            AppConnect.model0db = new Entities();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
            DataContext = curuser;
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

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Поиск работает по названию книги. Введите название книги в поле поиска и нажмите кнопку поиска или введите текст для автоматического обновления результатов.\n\n" +
                            "Сортировка позволяет упорядочить книги по названию, автору и цене. Выберите вариант сортировки в выпадающем списке и книги будут отсортированы в соответствии с выбранным параметром.",
                            "Информация о работе поиска и сортировки",
                            MessageBoxButton.OK, MessageBoxImage.Information);
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
            try
            {
                var button = sender as Button; var selectedBook = button.DataContext as Books;
                if (!App.Current.Properties.Contains("idUser"))
                {
                    MessageBox.Show("Ошибка: Не удалось получить ID пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (App.Current.Properties["idUser"] == null)
                {
                    MessageBox.Show("Ошибка: ID пользователя равен null.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(App.Current.Properties["idUser"].ToString(), out int userId))
                {
                    MessageBox.Show("Ошибка: Не удалось преобразовать ID пользователя в число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (selectedBook != null)
                {
                    using (var context = new BookStoreHEntities())
                    {
                        var user = context.Users.FirstOrDefault(u => u.idUser == userId);
                        if (user == null)
                        {
                            MessageBox.Show($"Ошибка: Пользователь с ID {userId} не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); return;
                        }
                        var order = context.Order.FirstOrDefault(o => o.idUser == userId && o.idStatus == 2); if (order == null)
                        {
                            order = new Order
                            {
                                idUser = userId,
                                idStatus = 2
                            }; context.Order.Add(order);
                            context.SaveChanges();
                        }
                        var cartItem = new Cart
                        {
                            idOrder = order.id,
                            idBooks = selectedBook.idBook
                        };
                        context.Cart.Add(cartItem); context.SaveChanges();
                        MessageBox.Show("Книга успешно добавлена в корзину!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите книгу из списка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении книги в корзину: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow();
            shopWindow.Show();
            this.Close();
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
