using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class SortLinesActionTest
    {
        [Test]
        public void When_Multiline_Input_Then_Sort()
        {
            Assert.AreEqual("a\r\nb", new SortLinesAction().PerformAction("b\r\na"));
        }

        [Test]
        public void When_Single_Input_Then_Return_Null()
        {
            Assert.IsNullOrEmpty(new SortLinesAction().PerformAction("aaaa\n"));
        }
    }
}
