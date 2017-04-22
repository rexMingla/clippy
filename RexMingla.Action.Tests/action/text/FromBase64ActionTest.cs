using NUnit.Framework;
using RexMingla.Action.action.text;

namespace RexMingla.Action.Tests.action.text
{
    [TestFixture]
    public class FromBase64ActionTest
    {
        [Test]
        public void When_Valid_Base64_Then_Convert_To_String()
        {
            Assert.AreEqual("test", new FromBase64Action().PerformAction("dGVzdA=="));
        }

        [Test]
        public void When_Invalid_Base64_Characters_Then_Return_Null()
        {
            Assert.IsNullOrEmpty(new FromBase64Action().PerformAction("$$$"));
        }

        [Test]
        public void When_Invalid_Base64_String_Then_Return_Null()
        {
            Assert.IsNullOrEmpty(new FromBase64Action().PerformAction("dGVzdA"));
        }
    }
}
