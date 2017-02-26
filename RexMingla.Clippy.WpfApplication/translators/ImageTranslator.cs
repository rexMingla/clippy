using RexMingla.ClipboardManager;
using System.Windows.Controls;
using System;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace RexMingla.Clippy.WpfApplication.translators
{
    public class ImageTranslator : ITranslator
    {
        private static int MaxIconHeight = 32;
        private static int MaxIconWidth = 128;

        string ITranslator.PreferredFormat
        {
            get
            {
                return "Bitmap";
            }
        }

        public MenuItem CreateMenuItem(ClipboardData data, ClipboardContent content)
        {
            var bitmap = data.Content as Bitmap;
            return new MenuItem
            {
                Header = $"Image [{bitmap.Width} x {bitmap.Height}]",
                Icon = new System.Windows.Controls.Image
                {
                    Source = ToBitmapImage(bitmap)
                },
                DataContext = content
            };
        }

        // http://stackoverflow.com/questions/6484357/converting-bitmapimage-to-bitmap-and-vice-versa
        private static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            var scale = Math.Max(bitmap.Width / MaxIconWidth, bitmap.Height / MaxIconHeight);
            var resizedBitmap = scale <= 1 ? bitmap : new Bitmap(bitmap, bitmap.Width / scale, bitmap.Height / scale);

            using (var memory = new MemoryStream())
            {
                resizedBitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
