using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class UpperCaseActionTest
    {
        [Test]
        public void When_Multi_Word_And_Uppercase_Then_Make_Uppercase()
        {
            Assert.AreEqual("UPPERCASE WORDS", new UpperCaseAction().PerformAction("uppercase words"));
        }
    }
}
