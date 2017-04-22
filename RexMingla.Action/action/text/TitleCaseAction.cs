using System.Globalization;

namespace RexMingla.Action.action.text
{
    public sealed class TitleCaseAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "Title Case"; } }

        public override string PerformAction(string oldValue)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(oldValue);
        }
    }
}
