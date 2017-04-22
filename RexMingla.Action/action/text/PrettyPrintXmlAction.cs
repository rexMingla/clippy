using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace RexMingla.Action.action.text
{
    public sealed class PrettyPrintXmlAction : AbstractAction<string, string>
    {
        protected override ContentTypeFormat ContentType { get { return ContentTypeFormat.Text; } }

        protected override string Label { get { return "Pretty Print Xml"; } }

        public override string PerformAction(string oldValue)
        {
            try
            {
                var stringBuilder = new StringBuilder();

                var element = XElement.Parse(oldValue);

                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.NewLineOnAttributes = false;

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    element.Save(xmlWriter);
                }

                return stringBuilder.ToString();
            }
            catch (XmlException)
            {
                return null;
            }
        }
    }
}
