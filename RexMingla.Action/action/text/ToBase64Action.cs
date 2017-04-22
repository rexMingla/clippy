using System;

namespace RexMingla.Action.action.text
{
    public sealed class ToBase64Action : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "To Base64"; } }

        public override string PerformAction(string oldValue)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(oldValue);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
