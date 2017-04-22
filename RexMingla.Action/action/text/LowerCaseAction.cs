namespace RexMingla.Action.action.text
{
    public sealed class LowerCaseAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "lowercase"; } }

        public override string PerformAction(string oldValue)
        {
            return oldValue.ToLower();
        }
    }
}
