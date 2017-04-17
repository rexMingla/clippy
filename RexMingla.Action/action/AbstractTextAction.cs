using RexMingla.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexMingla.Action.action
{
    public abstract class AbstractTextAction : IAction
    {
        protected abstract string Label { get; }

        protected abstract bool PerformTextAction(string oldValue, out string newValue);

        public ActionDetail PerformAction(ClipboardContent content)
        {
            var data = content.Data.FirstOrDefault(d => DataFormats.Contains(d.DataFormat));
            string newValue = null;
            if (data == null || !PerformTextAction(data.Content as string, out newValue))
            {
                return null;
            }
            return new ActionDetail
            {
                ActionLabel = Label,
                NewClipboardContent = UpdateTextContent(content, data.Content as string, newValue)
            };
        }

        private List<string> DataFormats = new List<string> { "System.String", "Text", "UnicodeText" };

        private ClipboardContent UpdateTextContent(ClipboardContent content, string oldValue, string newValue)
        {
            var ret = content.Clone();
            ret.Data.RemoveAll(d => !string.Equals(d.Content as string, oldValue));
            ret.Data.ForEach(d => d.Content = newValue);

            return ret;
        }
    }
}
