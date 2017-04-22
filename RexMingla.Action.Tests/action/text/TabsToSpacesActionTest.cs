using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class TabsToSpacesActionText
    {
        [Test]
        public void When_Tabs_Then_Replace_With_Spaces()
        {
            Assert.AreEqual("test  tab", new TabsToSpacesAction().PerformAction("test\ttab"));
        }
    }
}