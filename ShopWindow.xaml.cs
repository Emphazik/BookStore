using BookStore.AdminWindows;
using BookStore.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore
{
    public partial class ShopWindow : Window
    {
        public ShopWindow()
        {
            InitializeComponent();
            LoadCartItems();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            UserWindow user = new UserWindow(null);
            user.Show();
            this.Close();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button != null && button.Tag is int cartItemId)
                {
                    using (var context = new BookStoreHEntities())
                    {
                        var itemToRemove = context.Cart.FirstOrDefault(x => x.id == cartItemId);
                        if (itemToRemove != null)
                        {
                            context.Cart.Remove(itemToRemove);
                            context.SaveChanges();
                            LoadCartItems(); // Refresh the cart items list
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: Неверный формат ID элемента корзины.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении элемента из корзины: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreatePDF()
        {
            Document doc = new Document();
            try
            {
                string fileName = System.IO.Path.Combine("C:\\Users\\andre\\Downloads", $"order_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.Create));
                doc.Open();

                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\Arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 12);
                Font titleFont = new Font(baseFont, 18, Font.BOLD);

                doc.Add(new Paragraph("Список товаров", titleFont));

                int userId = Convert.ToInt32(App.Current.Properties["idUser"]);
                using (var context = new BookStoreHEntities())
                {
                    var order = context.Order.FirstOrDefault(o => o.idUser == userId && o.idStatus == 2);
                    if (order != null)
                    {
                        var cartItems = context.Cart.Where(c => c.idOrder == order.id).ToList();
                        decimal total = 0;
                        //string basePath = "C:\\Users\\andre\\Downloads\\BookStore\\BookStore\\BookStore";

                        foreach (var item in cartItems)
                        {
                            //string imagePath = Path.Combine(basePath + item.Books.ImageURL);

                            //if (!string.IsNullOrEmpty(item.Books.ImageURL) && File.Exists(imagePath))
                            //{
                            //    try
                            //    {
                            //        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                            //        img.ScaleAbsolute(100f, 100f); // Scale image
                            //        doc.Add(img);
                            //    }
                            //    catch (Exception imgEx)
                            //    {
                            //        MessageBox.Show("Ошибка при добавлении изображения: " + imgEx.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //    }
                            //}
                            //else
                            //{
                            //    MessageBox.Show("Изображение не найдено: " + imagePath, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            //}

                            doc.Add(new Paragraph("Название: " + item.Books.Title, font));
                            doc.Add(new Paragraph("Автор: " + item.Books.Authors.Name, font));
                            doc.Add(new Paragraph("Цена: " + item.Books.Price.ToString("F2") + " руб.", font));
                            doc.Add(new Paragraph("Описание: " + item.Books.Description, font));
                            doc.Add(new Paragraph(" ", font));
                            total += item.Books.Price;
                        }

                        doc.Add(new Paragraph($"Итого: {total.ToString("F2")} руб.", titleFont));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании PDF: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                doc.Close();
            }
        }

        private void UpdateOrderStatus()
        {
            int userId = Convert.ToInt32(App.Current.Properties["idUser"]);
            using (var context = new BookStoreHEntities())
            {
                var order = context.Order.FirstOrDefault(o => o.idUser == userId && o.idStatus == 2);
                if (order != null)
                {
                    order.idStatus = 1; 
                    context.SaveChanges();
                }
            }
        }

        private void ClearCart()
        {
            int userId = Convert.ToInt32(App.Current.Properties["idUser"]);
            using (var context = new BookStoreHEntities())
            {
                var order = context.Order.FirstOrDefault(o => o.idUser == userId && o.idStatus == 2);
                if (order != null)
                {
                    var cartItems = context.Cart.Where(c => c.idOrder == order.id).ToList();
                    context.Cart.RemoveRange(cartItems);
                    context.SaveChanges();
                }
            }
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите сформировать заказ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    CreatePDF();
                    UpdateOrderStatus();
                    ClearCart();
                    MessageBox.Show("PDF документ заказа успешно сформирован!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCartItems();
                    OrderWindow order = new OrderWindow();
                    order.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при формировании заказа: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoadCartItems()
        {
            int userId = Convert.ToInt32(App.Current.Properties["idUser"]);
            using (var context = new BookStoreHEntities())
            {
                var order = context.Order.FirstOrDefault(o => o.idUser == userId && o.idStatus == 2);
                if (order != null)
                {
                    var cartItems = context.Cart.Where(c => c.idOrder == order.id)
                        .Select(c => new CartViewModel
                        {
                            id = c.id,
                            Title = c.Books.Title,
                            Author = c.Books.Authors.Name,
                            Price = c.Books.Price,
                            ImageURL = c.Books.ImageURL
                        }).ToList();
                    cartListView.ItemsSource = cartItems;
                }
                else
                {
                    cartListView.ItemsSource = null;
                }
            }
        }

        public class CartViewModel
        {
            public int id { get; set; }
            public string Title { get; set; }
            public string Author { get; set; }
            public decimal Price { get; set; }
            public string ImageURL { get; set; }
        }
    }
}
