using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class TitleCaseActionTest
    {
        [Test]
        public void When_Multi_Word_Then_Capitalize_First_Letter_Of_Each_Word()
        {
            Assert.AreEqual("Capitalize Word", new TitleCaseAction().PerformAction("capitalize word"));
        }
    }
}
