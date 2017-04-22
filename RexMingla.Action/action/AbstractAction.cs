using RexMingla.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace RexMingla.Action.action
{
    public abstract class AbstractAction<InputType, OutputType> : IAction where InputType : class
    {
        protected abstract string Label { get; }

        protected abstract ContentTypeFormat ContentType { get; }

        public abstract OutputType PerformAction(InputType oldValue);

        public enum ContentTypeFormat
        {
            Text,
            File
        };

        private class ContentTypeMetadata
        {
            public List<string> InputDataFormats { get; set; }
            public List<string> OutputDataFormats { get; set; }
        };

        private static readonly List<string> TextDataFormats = new List<string> { "System.String", "Text", "UnicodeText" };

        private static readonly Dictionary<ContentTypeFormat, ContentTypeMetadata> Metadata = new Dictionary<ContentTypeFormat, ContentTypeMetadata>
        {
            {
                ContentTypeFormat.Text,
                new ContentTypeMetadata
                {
                   InputDataFormats = TextDataFormats,
                   OutputDataFormats = TextDataFormats
                }
            },
            {
                ContentTypeFormat.File,
                new ContentTypeMetadata
                {
                    InputDataFormats = new List<string> { "FileDrop" },
                    OutputDataFormats = TextDataFormats
                }
            }
        };

        public ActionDetail PerformAction(ClipboardContent content)
        {
            var meta = Metadata[ContentType];
            var data = content.Data.FirstOrDefault(d => meta.InputDataFormats.Contains(d.DataFormat));
            if (data == null)
            {
                return null;
            }
            var oldValue = data.Content as InputType;
            if (oldValue == null)
            {
                return null;
            }
            var newValue = PerformAction(oldValue);
            if (newValue == null)
            {
                return null;
            }
            return new ActionDetail
            {
                ActionLabel = Label,
                NewClipboardContent = CreateClipboardContent(content, newValue, meta.OutputDataFormats)
            };
        }

        private ClipboardContent CreateClipboardContent(ClipboardContent content, OutputType newValue, List<string> outputFormats)
        {
            return new ClipboardContent
            {
                Data = outputFormats.Select(f => new ClipboardData { DataFormat = f, Content = newValue }).ToList()
            };
        }

        public override string ToString() => Label;
    }
}
