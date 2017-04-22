namespace RexMingla.Action.action.text
{
    public sealed class UpperCaseAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "UPPERCASE"; } }

        public override string PerformAction(string oldValue)
        {
            return oldValue.ToUpper();
        }
    }
}
