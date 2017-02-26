using Common.Logging;
using RexMingla.ClipboardManager;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RexMingla.Clippy.WpfApplication
{
    public static class MenuItemTranslator
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static int MaxIconHeight = 32;
        private static int MaxIconWidth = 128;

        public static MenuItem ToMenuItem(this ClipboardContent content)
        {
            var type = GetType(content);
            try
            { 
                var data = content.Data.First(d => d.DataFormat == type);
                switch (type)
                {
                    case "Text":
                        var text = data.Content as string;
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            return new MenuItem
                            {
                                Header = "[Text] white space only",
                                DataContext = content
                            };
                        }
                        var trimmedText = text.Trim();
                        return new MenuItem
                        {
                            Header = trimmedText.Length > 100 ? $"{trimmedText.Substring(0, 97)}..." : trimmedText,
                            DataContext = content
                        };
                    case "Bitmap":
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
                    case "FileDrop":
                        var names = data.Content as string[];
                        if (names.Count() == 1)
                        {
                            var name = names.First();
                            return new MenuItem
                            {
                                Header = name.Length > 100 ? $"[1 File] ...{name.Substring(name.Length - 97, name.Length)}" : $"[1 File] {name}",
                                DataContext = content
                            };
                        }
                        return new MenuItem
                        {
                            Header = $"[{names.Count()} Files]",
                            DataContext = content
                        };
                    default:
                        _log.Warn($"Clipboard data type {type} unknown.");
                        break;
                }
            }
            catch (Exception ex)
            { 
                _log.Error($"Error creating menu item of type {type}.", ex);
            }
            return new MenuItem
            {
                Header = $"[{type}]",
                DataContext = content
            };
        }

        private static string GetType(ClipboardContent content)
        {
            return content.Data.Any(d => d.DataFormat == "Text") ? "Text" : content.Data.First().DataFormat;
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
