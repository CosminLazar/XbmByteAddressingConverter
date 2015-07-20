using NUnit.Framework;
using xbmImage;

namespace Test
{
    [TestFixture]
    public class ConversionTests
    {
        [Test]
        public void CanConvertToHorizontal()
        {
            var actualHorizontal = XbmByteAddressingConverter.ToHorizontalAddressing(XbmData.VerticalAddressedImage, XbmData.Width, XbmData.Height);
            var expectedHorizontal = XbmData.HorizontalAddressedImage;

            CollectionAssert.AreEqual(expectedHorizontal, actualHorizontal);
        }

        [Test]
        public void CanConvertToVertical()
        {
            var actualVertical = XbmByteAddressingConverter.ToVerticalAddressing(XbmData.HorizontalAddressedImage, XbmData.Width, XbmData.Height);
            var expectedVertical = XbmData.VerticalAddressedImage;

            CollectionAssert.AreEqual(expectedVertical, actualVertical);
        }
    }
}
