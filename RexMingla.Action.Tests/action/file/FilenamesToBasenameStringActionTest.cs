using NUnit.Framework;
using RexMingla.Action.action.file;

namespace RexMingla.Action.Tests.action.file
{
    [TestFixture]
    public class FilenamesToBasenameStringActionTest
    {
        [Test]
        public void When_Files_Then_Return_New_Line_Delimited_Basename_String()
        {
            Assert.AreEqual("a\r\nb", new FilenamesToBasenameStringAction().PerformAction(new string[] { @"c:\a", @"c:\b" }));
        }
    }
}
