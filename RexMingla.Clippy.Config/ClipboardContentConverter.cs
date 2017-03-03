using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RexMingla.ClipboardManager;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace RexMingla.Clippy.Config
{
    public sealed class ClipboardContentConverter : JsonConverter
    {
        public ClipboardContentConverter()
        {
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ClipboardContent);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var content = new ClipboardContent();
            JObject jObject = JObject.Load(reader);
            serializer.Populate(jObject.CreateReader(), content);

            var bitmap = content.Data?.FirstOrDefault(d => d.DataFormat == "Bitmap");
            if (bitmap != null)
            {
                bitmap.Content = DeserializeImage(bitmap.Content.ToString());
            }
            return content;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var strippedContent = value as ClipboardContent;
            if (strippedContent == null)
            {
                return;
            }
            strippedContent.Data.RemoveAll(d => d.Content as MemoryStream != null); // remove for purposes of serialization

            var bitmap = strippedContent.Data.FirstOrDefault(d => d.DataFormat == "Bitmap");
            if (bitmap != null)
            {
                bitmap.Content = SerializeImage(bitmap.Content);
            }

            var jsonObject = JObject.FromObject(strippedContent);
            jsonObject.WriteTo(writer);
        }
        
        private static Bitmap DeserializeImage(string jsonString)
        {
            var m = new MemoryStream(Convert.FromBase64String(jsonString));
            return (Bitmap)Image.FromStream(m);
        }

        private static object SerializeImage(object obj)
        {
            var bmp = obj as Bitmap;
            if (bmp == null)
            {
                return null;
            }
            var m = new MemoryStream();
            bmp.Save(m, System.Drawing.Imaging.ImageFormat.Jpeg);

            return Convert.ToBase64String(m.ToArray());
        }
    }
}
