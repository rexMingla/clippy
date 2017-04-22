using System;

namespace RexMingla.Action.action.text
{
    public sealed class FromBase64Action : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "From Base64"; } }

        public override string PerformAction(string oldValue)
        {
            try
            {
                var bytes = Convert.FromBase64String(oldValue);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch(FormatException)
            {
                return null;
            }
        }
    }
}
