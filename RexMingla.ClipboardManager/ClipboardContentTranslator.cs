using System.Linq;
using System.Windows.Forms;

namespace RexMingla.ClipboardManager
{
    public static class ClipboardContentTranslator
    {
        public static IDataObject ToIDataObject(this ClipboardContent content)
        {
            if (content == null)
            {
                return null;
            }
            var ret = new DataObject();
            foreach (var d in content.Data)
            {
                ret.SetData(d.DataFormat, d.Content);
            }
            return ret;
        }

        public static ClipboardContent ToClipboardContent(this IDataObject obj)
        {
            if (obj == null)
            {
                return null;
            }
            return new ClipboardContent
            {
                Data = obj.GetFormats().OrderBy(f => f).Select(f => new ClipboardData { DataFormat = f, Content = obj.GetData(f) }).ToList()
            };
        }
    }
}
