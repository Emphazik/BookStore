using BookStore.ApplicationData;
using BookStore.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace BookStore.AdminWindows
{
    public partial class EditBooks : Window
    {
        private Books bookToEdit;

        private List<Categories> categories = AppConnect.bookStoreHEntities.Categories.ToList();
        private List<Authors> authors = AppConnect.bookStoreHEntities.Authors.ToList();
        private List<Publishers> publishers = AppConnect.bookStoreHEntities.Publishers.ToList();

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
            PublisherComboBox.ItemsSource = publishers;

            AuthorComboBox.ItemsSource = authors;

            CategoryComboBox.ItemsSource = categories;
        }

        private void FillFields()
        {
            if (bookToEdit != null)
            {
                ISBNTextBox.Text = bookToEdit.ISBN;
                TitleTextBox.Text = bookToEdit.Title;
                YearTextBox.Text = bookToEdit.year_of_publication.ToString();

                PublisherComboBox.SelectedIndex = publishers.FindIndex(c => c.idPublisher == bookToEdit.idPublisher);
                AuthorComboBox.SelectedIndex = authors.FindIndex(c => c.idAuthor == bookToEdit.idAuthor);
                CategoryComboBox.SelectedIndex = categories.FindIndex(c => c.idCategory == bookToEdit.idCategory);

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
                                existingBook.idPublisher = publishers[PublisherComboBox.SelectedIndex].idPublisher;
                            if (AuthorComboBox.SelectedValue != null)
                                existingBook.idAuthor = authors[AuthorComboBox.SelectedIndex].idAuthor;
                            if (CategoryComboBox.SelectedValue != null)
                                existingBook.idCategory = categories[CategoryComboBox.SelectedIndex].idCategory;

                            existingBook.Size = SizeTextBox.Text;
                            existingBook.Weight = decimal.Parse(WeightTextBox.Text);
                            existingBook.Pages = int.Parse(PagesTextBox.Text);
                            existingBook.Price = decimal.Parse(PriceTextBox.Text);
                            existingBook.Description = DescriptionTextBox.Text;
                            existingBook.ImageURL = ImageURLTextBox.Text;

                            context.SaveChanges();
                            MessageBox.Show("Информация о книге обновлена успешно");
                            
                            new MainWindow().Show();
                            this.Close();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resourses", fileName);

                try
                {
                    string resoursesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resourses");
                    if (!Directory.Exists(resoursesFolder))
                    {
                        Directory.CreateDirectory(resoursesFolder);
                    }

                    new FileInfo(filePath).CopyTo(destPath, true);
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
