namespace RexMingla.Action.action.file
{
    public sealed class FilenamesToStringAction : AbstractAction<string[], string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.File; } }

        protected override string Label { get { return "Filename(s) to string(s)"; } }

        public override string PerformAction(string[] oldValue)
        {
            return string.Join("\r\n", oldValue);
        }
    }
}
