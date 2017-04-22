using System.Linq;

namespace RexMingla.Action.action.file
{
    public sealed class FilenamesToBasenameStringAction : AbstractAction<string[], string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.File; } }

        protected override string Label { get { return "Filename(s) to basenames(s)"; } }

        public override string PerformAction(string[] oldValue)
        {
            return string.Join("\r\n", oldValue.Select(x => System.IO.Path.GetFileName(x)));
        }
    }
}
