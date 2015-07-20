using System;
using System.Collections.Generic;

namespace xbmImage
{
    public class XbmByteAddressingConverter
    {
        public static byte[] ToHorizontalAddressing(byte[] verticalAddressedData, int width, int height)
        {
            if (width * height != verticalAddressedData.Length * 8)
                throw new ArgumentException("Width and Height do not match the specified data!");

            var rawImage = RawImageFromVerticalAddressed(verticalAddressedData, width, height);

            var horizontalAddressedData = new List<byte>(verticalAddressedData.Length);

            for (var i = 0; i < height; i++)
            {
                var byteStr = "";
                for (var j = 0; j < width; j++)
                {
                    byteStr = (rawImage[i, j] ? "1" : "0") + byteStr;

                    if ((j + 1) % 8 == 0)
                    {
                        horizontalAddressedData.Add(Convert.ToByte(byteStr, 2));
                        byteStr = "";
                    }
                }
            }

            return horizontalAddressedData.ToArray();
        }

        public static byte[] ToVerticalAddressing(byte[] horizontalAddressedData, int width, int height)
        {
            if (width * height != horizontalAddressedData.Length * 8)
                throw new ArgumentException("Width and Height do not match the specified data!");

            var rawImage = RawImageFromHorizontalAddressed(horizontalAddressedData, width, height);
            var verticalAddressedData = new List<byte>(horizontalAddressedData.Length);

            for (var i = 0; i < height; i += 8)
            {
                for (var j = 0; j < width; j++)
                {
                    var byteStr = "";
                    for (var k = 0; k < 8; k++)
                    {
                        byteStr = (rawImage[i + k, j] ? "1" : "0") + byteStr;
                    }
                    verticalAddressedData.Add(Convert.ToByte(byteStr, 2));
                }
            }

            return verticalAddressedData.ToArray();
        }

        private static bool[,] RawImageFromVerticalAddressed(IEnumerable<byte> verticalAddressedData, int width, int height)
        {
            var screenPixells = new bool[height, width];

            var verticalIndex = 0;
            var horizontalIndex = 0;

            foreach (var item in verticalAddressedData)
            {
                var itemScreenData = Convert.ToString(item, 2);

                //itemScreenData is rendered from least significant to most significant bit
                var itemVerticalOffset = 0;
                for (var i = itemScreenData.Length - 1; i >= 0; i--)
                {
                    screenPixells[verticalIndex + itemVerticalOffset, horizontalIndex] = itemScreenData[i] == '1';
                    itemVerticalOffset++;
                }

                horizontalIndex++;
                if (horizontalIndex == width)
                {
                    verticalIndex += 8;
                    horizontalIndex = 0;
                }
            }

            return screenPixells;
        }

        private static bool[,] RawImageFromHorizontalAddressed(IEnumerable<byte> horizontalAddressedData, int width, int height)
        {
            var screenPixells = new bool[height, width];

            var verticalIndex = 0;
            var horizontalIndex = 0;

            foreach (var item in horizontalAddressedData)
            {
                var itemScreenData = Convert.ToString(item, 2).PadLeft(8, '0');

                //itemScreenData is rendered from least significant to most significant bit                
                for (var i = itemScreenData.Length - 1; i >= 0; i--)
                {
                    screenPixells[verticalIndex, horizontalIndex] = itemScreenData[i] == '1';
                    horizontalIndex++;
                }

                if (horizontalIndex == width)
                {
                    verticalIndex++;
                    horizontalIndex = 0;
                }
            }

            return screenPixells;
        }
    }
}