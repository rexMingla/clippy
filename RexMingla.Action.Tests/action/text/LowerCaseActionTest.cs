using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class LowerCaseActionTest
    {
        [Test]
        public void When_Multi_Word_And_Lowercase_Then_Make_Lowercase()
        {
            Assert.AreEqual("lowercase words", new LowerCaseAction().PerformAction("LOWERCASE WORDS"));
        }
    }
}
