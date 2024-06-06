using BookStore.ApplicationData;
using BookStore.Models;
using System;
using System.Linq;
using System.Windows;

namespace BookStore.AdminWindows
{
    public partial class EditUser : Window
    {
        private readonly UserViewModel userToEdit;

        public EditUser(UserViewModel user)
        {
            InitializeComponent();
            userToEdit = user;
            LoadRoles();
            FillFields();
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

        private void FillFields()
        {
            if (userToEdit != null)
            {
                UsernameTextBox.Text = userToEdit.Username;
                PasswordBox.Password = userToEdit.Password;
                LoginTextBox.Text = userToEdit.Login;
                PhoneTextBox.Text = userToEdit.Phone;
                EmailTextBox.Text = userToEdit.Email;
                RoleComboBox.SelectedValue = userToEdit.Role; // Устанавливаем идентификатор роли
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (userToEdit != null)
            {
                userToEdit.Username = UsernameTextBox.Text;
                userToEdit.Password = PasswordBox.Password;
                userToEdit.Login = LoginTextBox.Text;
                userToEdit.Phone = PhoneTextBox.Text;
                userToEdit.Email = EmailTextBox.Text;
                userToEdit.Role = (int)RoleComboBox.SelectedValue;

                using (var context = new BookStoreHEntities())
                {
                    var userInDb = context.Users.FirstOrDefault(u => u.idUser == userToEdit.IdUser);
                    if (userInDb != null)
                    {
                        userInDb.Username = userToEdit.Username;
                        userInDb.Password = userToEdit.Password;
                        userInDb.Login = userToEdit.Login;
                        userInDb.Phone = userToEdit.Phone;
                        userInDb.Email = userToEdit.Email;
                        userInDb.Role = userToEdit.Role;

                        context.SaveChanges();
                        MessageBox.Show("Изменения сохранены успешно.");
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка при получении данных пользователя.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            UsersList usersList = new UsersList();
            usersList.Show();
            this.Close();
        }
    }
}
