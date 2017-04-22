using NUnit.Framework;
using RexMingla.Action.action.file;

namespace RexMingla.Action.Tests.action.file
{
    [TestFixture]
    public class FilenamesToStringActionTest
    {
        [Test]
        public void When_Files_Then_Return_New_Line_Delimited_String()
        {
            Assert.AreEqual("a\r\nb", new FilenamesToStringAction().PerformAction(new string[] { "a", "b" }));
        }
    }
}
