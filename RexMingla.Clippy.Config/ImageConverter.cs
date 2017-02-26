using System;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;

namespace RexMingla.Clippy.Config
{
    public class ImageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Bitmap);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var m = new MemoryStream(Convert.FromBase64String((string)reader.Value));
            return Bitmap.FromStream(m);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bmp = value as Bitmap;
            var m = new MemoryStream();
            bmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);

            writer.WriteValue(Convert.ToBase64String(m.ToArray()));
        }
    }
}
