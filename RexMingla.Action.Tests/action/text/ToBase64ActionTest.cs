using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class ToBase64ActionTest
    {
        [Test]
        public void When_String_Then_Convert_To_Base64()
        {
            Assert.AreEqual("dGVzdA==", new ToBase64Action().PerformAction("test"));
        }
    }
}
