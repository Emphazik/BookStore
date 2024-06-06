using BookStore.ApplicationData;
using BookStore.Models;
using System;
using System.Linq;
using System.Windows;

namespace BookStore.AdminWindows
{
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            using (var context = new BookStoreHEntities())
            {
                var roles = context.Roles.ToList();
                RoleComboBox.ItemsSource = roles;
                RoleComboBox.DisplayMemberPath = "nameRole";
                RoleComboBox.SelectedValuePath = "idRole";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string login = LoginTextBox.Text;
            string phone = PhoneTextBox.Text;
            string email = EmailTextBox.Text;
            int roleId = (int)RoleComboBox.SelectedValue;

            Users newUser = new Users
            {
                Username = username,
                Password = password,
                Login = login,
                Phone = phone,
                Email = email,
                Role = roleId
            };

            using (var context = new BookStoreHEntities())
            {
                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Пользователь успешно добавлен!");
            }

            this.Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UsersList usersList = new UsersList();
            usersList.Show();
            this.Close();
        }
    }
}
