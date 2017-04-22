namespace RexMingla.Action.action.text
{
    public sealed class TabsToSpacesAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "Replace tabs with spaces"; } }

        public override string PerformAction(string oldValue)
        {
            return oldValue.Replace("\t", "  ");
        }
    }
}
