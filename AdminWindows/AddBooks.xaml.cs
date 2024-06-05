using BookStore.ApplicationData;
using BookStore.Models;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BookStore.AdminWindows
{
    public partial class AddBooks : Window
    {
        public static BookStoreHEntities bookStoreHEntities;

        public object TestGrid { get; private set; }

        public AddBooks()
        {
            InitializeComponent();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            PublisherComboBox.ItemsSource = AppConnect.bookStoreHEntities.Publishers.ToList();
            PublisherComboBox.DisplayMemberPath = "Name";
            PublisherComboBox.SelectedValuePath = "idPublisher";

            AuthorComboBox.ItemsSource = AppConnect.bookStoreHEntities.Authors.ToList();
            AuthorComboBox.DisplayMemberPath = "Name";
            AuthorComboBox.SelectedValuePath = "idAuthor";

            CategoryComboBox.ItemsSource = AppConnect.bookStoreHEntities.Categories.ToList();
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "idCategory";

            // Убедитесь, что хотя бы один элемент выбран по умолчанию
            if (PublisherComboBox.Items.Count > 0)
            {
                PublisherComboBox.SelectedIndex = 0;
            }

            if (AuthorComboBox.Items.Count > 0)
            {
                AuthorComboBox.SelectedIndex = 0;
            }

            if (CategoryComboBox.Items.Count > 0)
            {
                CategoryComboBox.SelectedIndex = 0;
            }
        }


        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            // Преобразование значений из текстовых полей в нужные типы
            if (int.TryParse(YearTextBox.Text, out int year) &&
                int.TryParse(WeightTextBox.Text, out int weight) &&
                int.TryParse(PagesTextBox.Text, out int pages) &&
                decimal.TryParse(PriceTextBox.Text, out decimal price))
            {
                if (PublisherComboBox.SelectedValue == null ||
                    AuthorComboBox.SelectedValue == null ||
                    CategoryComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Пожалуйста, выберите издательство, автора и категорию.");
                    return;
                }

                Books newBook = new Books
                {
                    ISBN = ISBNTextBox.Text,
                    Title = TitleTextBox.Text,
                    year_of_publication = year,
                    idPublisher = (int)PublisherComboBox.SelectedValue,
                    idAuthor = (int)AuthorComboBox.SelectedValue,
                    idCategory = (int)CategoryComboBox.SelectedValue,
                    Size = SizeTextBox.Text,
                    Weight = weight,
                    Pages = pages,
                    Price = price,
                    Description = DescriptionTextBox.Text,
                    ImageURL = ImageURLTextBox.Text
                };

                AppConnect.bookStoreHEntities.Books.Add(newBook);
                AppConnect.bookStoreHEntities.SaveChanges();

                MessageBox.Show("Книга успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите правильные значения для года, веса, страниц и цены.");
            }
        }



        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resourses", fileName);

                try
                {
                    // Проверяем, существует ли папка Resourses
                    string resoursesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resourses");
                    if (!Directory.Exists(resoursesFolder))
                    {
                        Directory.CreateDirectory(resoursesFolder);
                    }

                    // Копируем изображение в папку Resourses
                    File.Copy(filePath, destPath, true);
                    ImageURLTextBox.Text = $"/Resourses/{fileName}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при копировании изображения: {ex.Message}");
                }
            }
        }



    }
}
