using BookStore.ApplicationData;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BookStore.ManagerWindows
{
    /// <summary>
    /// Логика взаимодействия для Manager1.xaml
    /// </summary>
    public partial class Manager1 : Window
    {
        public Manager1()
        {
            InitializeComponent();
            List<Order> orders = AppConnect.bookStoreHEntities.Order.ToList();
            orderslist.ItemsSource = orders;
        }


        private void AboutOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
