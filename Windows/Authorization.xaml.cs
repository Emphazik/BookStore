using BookStore.AdminWindows;
using BookStore.ApplicationData;
using BookStore.ManagerWindows;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
            AppConnect.model0db = new Entities();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
        }

        private void btnAuthorize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Password))
                {
                    MessageBox.Show("Все поля должны быть заполнены.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (txtLogin.Text.Length > 20 || txtPassword.Password.Length > 20)
                {
                    MessageBox.Show("Поле логина и пароля не должно превышать 20 символов.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = AppConnect.bookStoreHEntities.Users.FirstOrDefault(x => x.Login == txtLogin.Text);
                if (user == null)
                {
                    MessageBox.Show("Такого пользователя не существует.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var password = AppConnect.bookStoreHEntities.Users.FirstOrDefault(x => x.Password == txtPassword.Password);
                if (password == null)
                {
                    MessageBox.Show("Неверный пароль.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    switch (user.Role)
                    {
                        case 1:
                            MessageBox.Show($"Приветствую Администратора, {user.Username}",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information); ;
                            MainWindow main = new MainWindow();
                            main.Show();
                            this.Close();
                            break;
                        case 2:
                            App.Current.Properties["idUser"] = user.idUser;
                            MessageBox.Show($"Приветствую Пользователя, {user.Username}",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            UserWindow main1 = new UserWindow((sender as Button).DataContext as Users);
                            main1.Show();
                            this.Close();
                            break;
                        case 3:
                            MessageBox.Show($"Приветствую Менеджера, {user.Username}",
                   "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            Manager1 manager = new Manager1();
                            manager.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Данные не обнаружены!",
                            "уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration register = new Registration();
            register.Show();
            this.Close();
        }
    }
}
