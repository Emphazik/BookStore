using BookStore.ApplicationData;
using BookStore.Models;
using System;
using System.Linq;
using System.Windows;

namespace BookStore.AdminWindows
{
    public partial class EditBooks : Window
    {
        private Books bookToEdit;

        public EditBooks(Books book)
        {
            InitializeComponent();
            AppConnect.bookStoreHEntities = new BookStoreHEntities();
            bookToEdit = book;
            LoadComboBoxData();
            FillFields();
        }

        private void LoadComboBoxData()
        {
            PublisherComboBox.ItemsSource = AppConnect.bookStoreHEntities.Publishers.ToList();
            PublisherComboBox.DisplayMemberPath = "Name";
            PublisherComboBox.SelectedValuePath = "Id";

            AuthorComboBox.ItemsSource = AppConnect.bookStoreHEntities.Authors.ToList();
            AuthorComboBox.DisplayMemberPath = "Name";
            AuthorComboBox.SelectedValuePath = "Id";

            CategoryComboBox.ItemsSource = AppConnect.bookStoreHEntities.Categories.ToList();
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";
        }

        private void FillFields()
        {
            if (bookToEdit != null)
            {
                ISBNTextBox.Text = bookToEdit.ISBN;
                TitleTextBox.Text = bookToEdit.Title;
                YearTextBox.Text = bookToEdit.year_of_publication.ToString();

                PublisherComboBox.SelectedValue = bookToEdit.idPublisher ?? 0;
                AuthorComboBox.SelectedValue = bookToEdit.idAuthor ?? 0;
                CategoryComboBox.SelectedValue = bookToEdit.idCategory ?? 0;

                SizeTextBox.Text = bookToEdit.Size;
                WeightTextBox.Text = bookToEdit.Weight.ToString();
                PagesTextBox.Text = bookToEdit.Pages.ToString();
                PriceTextBox.Text = bookToEdit.Price.ToString();
                DescriptionTextBox.Text = bookToEdit.Description;
                ImageURLTextBox.Text = bookToEdit.ImageURL;
            }
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (bookToEdit != null)
            {
                try
                {
                    using (var context = new BookStoreHEntities())
                    {
                        Books existingBook = context.Books.FirstOrDefault(b => b.idBook == bookToEdit.idBook);
                        if (existingBook != null)
                        {
                            existingBook.ISBN = ISBNTextBox.Text;
                            existingBook.Title = TitleTextBox.Text;
                            existingBook.year_of_publication = int.Parse(YearTextBox.Text);

                            if (PublisherComboBox.SelectedValue != null)
                                existingBook.idPublisher = (int)PublisherComboBox.SelectedValue;
                            if (AuthorComboBox.SelectedValue != null)
                                existingBook.idAuthor = (int)AuthorComboBox.SelectedValue;
                            if (CategoryComboBox.SelectedValue != null)
                                existingBook.idCategory = (int)CategoryComboBox.SelectedValue;

                            existingBook.Size = SizeTextBox.Text;
                            existingBook.Weight = decimal.Parse(WeightTextBox.Text);
                            existingBook.Pages = int.Parse(PagesTextBox.Text);
                            existingBook.Price = decimal.Parse(PriceTextBox.Text);
                            existingBook.Description = DescriptionTextBox.Text;
                            existingBook.ImageURL = ImageURLTextBox.Text;

                            context.SaveChanges();
                            MessageBox.Show("Информация о книге обновлена успешно");
                            DialogResult = true; // Закройте окно после сохранения изменений
                        }
                        else
                        {
                            MessageBox.Show("Книга не найдена");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
                }
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
            // Обработка загрузки изображения
        }
    }
}
