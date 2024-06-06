using BookStore.ApplicationData;
using BookStore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.AdminWindows
{
    public partial class UsersList : Window
    {
        public UsersList()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (var context = new BookStoreHEntities())
            {
                var usersList = context.Users.Select(u => new UserViewModel
                {
                    IdUser = u.idUser, 
                    Username = u.Username,
                    Password = u.Password,
                    Login = u.Login,
                    Phone = u.Phone,
                    Email = u.Email,
                    RoleName = u.Roles.nameRole,
                    Role = u.Role 
                }).ToList();

                userListView.ItemsSource = usersList;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUserWindow = new AddUser(); 
            addUserWindow.ShowDialog();
            LoadUsers(); 
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var user = button.DataContext as UserViewModel;
                if (user != null)
                {
                    EditUser editUserWindow = new EditUser(user);
                    editUserWindow.ShowDialog();
                    LoadUsers();
                }
            }
        }


        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                var user = button.DataContext as UserViewModel;
                if (user != null)
                {
                    var result = MessageBox.Show($"Вы точно хотите удалить пользователя {user.Username}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (var context = new BookStoreHEntities())
                        {
                            var userToDelete = context.Users.FirstOrDefault(u => u.Username == user.Username);
                            if (userToDelete != null)
                            {
                                context.Users.Remove(userToDelete);
                                context.SaveChanges();
                            }
                        }
                        LoadUsers(); 
                    }
                }
            }
        }
    }

    public class UserViewModel
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
        public int Role { get; set; }
    }
}
