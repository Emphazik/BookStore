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
using System.Windows.Shapes;

namespace BookStore.Windows
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            AppConnect.model0db = new Entities();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
        }

        private void btnAuthorize1_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            auth.Show();
            this.Close();
        }

        public void registration_process()
        {
            try
            {
                Users user = new Users()
                {
                    Login = txtLogin.Text,
                    Username = txtName.Text,
                    Password = txtPassword.Password,
                    Role = 2,
                    Email = txtEmail.Text,
                    Phone = txtPhone.Text 
                };
                AppConnect.bookStoreHEntities.Users.Add(user);
                AppConnect.bookStoreHEntities.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                Authorization auth = new Authorization();
                auth.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Ошибка добавления данных",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnRegister1_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                MessageBox.Show("Все поля должны быть заполнены.",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (txtConfirmPassword.Password != txtPassword.Password || string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                btnRegister1.IsEnabled = false;
            }
            else
            {
                btnRegister1.IsEnabled = true;
            }
            //------------------------------------------------------------------------------------------------
            if (AppConnect.bookStoreHEntities.Users.Count(x => x.Login == txtLogin.Text) > 0)
            {
                MessageBox.Show("Такой пользователь уже существует!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //------------------------------------------------------------------------------------------------
            if (txtEmail.Text.Contains("@"))
            {
                if (txtPhone.Text.Length < 10 || !txtPhone.Text.Contains("+") || txtPhone.Text.Length > 15)
                {
                    MessageBox.Show("Неверный формат телефона!",
                           "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    registration_process();
                }
            }
            else
            {
                MessageBox.Show("Неверный формат почты",
                       "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
