using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class PrettyPrintXmlActionTest
    {
        [Test]
        public void When_Invalid_Xml_Then_Return_Null()
        {
            Assert.IsNullOrEmpty(new PrettyPrintXmlAction().PerformAction("<xml>"));
        }

        [Test]
        public void When_Valid_Xml_And_No_Header_Then_Return_Formatted_String()
        {
            Assert.AreEqual("<xml>\r\n  <test />\r\n</xml>", new PrettyPrintXmlAction().PerformAction("<xml><test /></xml>"));
        }

        [Test]
        public void When_Valid_Xml_And_Header_Then_Return_Formatted_String()
        {
            Assert.AreEqual("<xml>\r\n  <test />\r\n</xml>", new PrettyPrintXmlAction().PerformAction("<?xml version='1.0'?><xml><test /></xml>"));
        }
    }
}