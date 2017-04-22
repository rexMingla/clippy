using System;
using System.Linq;

namespace RexMingla.Action.action.text
{
    public sealed class SortLinesAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "Sort Lines"; } }

        public override string PerformAction(string oldValue)
        {
            const string newLine = "\r\n";
            return !oldValue.Contains(newLine) ? null : string.Join(newLine, oldValue.Split(new string[] { newLine }, StringSplitOptions.RemoveEmptyEntries).OrderBy(s => s));
        }
    }
}
