# XbmByteAddressingConverter
Converts the byte addressing of an xbm image from horizontal to vertical and vice versa.

## converting from horizontal to vertical addressing

```c#
var verticalAddressedImage = XbmByteAddressingConverter.ToVerticalAddressing(XbmData.HorizontalAddressedImage, XbmData.Width, XbmData.Height);
```

## converting from vertical to horizontal addressing

```c#
var horizontalAddressedImage = XbmByteAddressingConverter.ToHorizontalAddressing(XbmData.VerticalAddressedImage, XbmData.Width, XbmData.Height);
```