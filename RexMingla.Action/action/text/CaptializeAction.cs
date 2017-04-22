namespace RexMingla.Action.action.text
{
    public sealed class CaptializeAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "Capitalize"; } }

        public override string PerformAction(string oldValue)
        {
            return $"{char.ToUpper(oldValue[0])}{oldValue.Substring(1)}";
        }
    }
}
