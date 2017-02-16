using System.Linq;
using NUnit.Framework;

namespace RexMingla.ClipboardManager.Tests
{
    [TestFixture]
    public class ClipboardStoreTest
    {
        ClipboardContent c1 = CreateClipboardContent(new ClipboardData { Content = "test", DataFormat = "Text" });
        ClipboardContent c2 = CreateClipboardContent(new ClipboardData { Content = "test", DataFormat = "Text" });
        ClipboardContent c3 = CreateClipboardContent(new ClipboardData { Content = "testy", DataFormat = "Text" });

        [Test]
        public void When_New_Item_Added_To_Store_Then_Increment_Count()
        {
            var store = new ClipboardStore();
            store.InsertItem(c1);
            Assert.AreEqual(1, GetStoreSize(store));
            
            store.InsertItem(c3);
            Assert.AreEqual(2, GetStoreSize(store));
        }

        [Test]
        public void When_Same_Item_Added_To_Store_Then_Count_Remains_Same()
        {
            var store = new ClipboardStore();
            store.InsertItem(c1);
            Assert.AreEqual(1, GetStoreSize(store));

            store.InsertItem(c2);
            Assert.AreEqual(1, GetStoreSize(store));
        }

        private static ClipboardContent CreateClipboardContent(params ClipboardData[] data)
        {
            return new ClipboardContent { Data = data.ToList() };
        } 

        private static int GetStoreSize(ClipboardStore store)
        {
            return store.GetItems().Count;
        }
    }
}
