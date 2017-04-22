using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class CapitalizeActionTest
    {
        [Test]
        public void When_Multi_Word_Then_Capitalize_First_Letter_Only()
        {
            Assert.AreEqual("Capitalize word", new CaptializeAction().PerformAction("capitalize word"));
        }
    }
}