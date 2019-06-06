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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp10.Models;
using ZXing;


namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для Window5.xaml
    /// </summary>
    public partial class Window5 : MetroWindow
    {
        public Window5(Lector lector)
        {
            InitializeComponent();
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    CharacterSet = "utf-8",
                    Height = 500,
                    Width = 500,
                    Margin = 1


                }

            };

            System.Drawing.Bitmap image = barcodeWriter.Write(lector.UniqueCode);

            pictureBox1.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());


        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
        public class Lector
        {
            public string Name { get; set; }
            public string TableName { get; set; }
            public string Subject { get; set; }
            public string UniqueCode { get; set; }

        }
    }
}
