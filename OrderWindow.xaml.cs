using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Aspose.BarCode.Generation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = iTextSharp.text.Image;
using System.Windows.Markup;
using System.IO;
using System.Xml.Linq;
using Paragraph = iTextSharp.text.Paragraph;

namespace BookStore
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        public OrderWindow()
        {
            InitializeComponent();
        }

        int a = 0;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://sun9-18.userapi.com/impg/ywhFq4Vkya7zqtF82qNYNVz6vHcaZB89_q0ZOA/M1-viAOAtWE.jpg?size=1280x720&quality=96&sign=24a959f6e0bb720e1153e0304998d9ef&c_uniq_tag=BQ38ZOPAcUmfbizZA-J8nQvUckoz7wA46rInjgsDPSY&type=album");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            string dataDir = @"C:\Users\andre\Downloads\BookStore\BookStore\BookStore\";
            gen.Save(dataDir + a.ToString() + "1.png", BarCodeImageFormat.Png);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(dataDir + a.ToString() + "1.png");
            bitmap.EndInit();
            image.Source = bitmap;
            a++;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
