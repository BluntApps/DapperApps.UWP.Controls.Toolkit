using Windows.Foundation;
using Windows.UI.Xaml.Media.Imaging;

//namespace DapperApps.Media.Imaging
//{
//    public static class WriteableBitmapEx
//    {
//        public static Rect GetBounded(this WriteableBitmap wb, int aRGBThreshold)
//        {
//            int[] pixels = wb.Pixels;
//            int width = wb.PixelWidth;
//            int height = wb.PixelHeight;

//            int leftIndex = (height / 2) * width;
//            int topIndex = width / 2;
//            int rightIndex = (width * (height / 2 + 1)) - 1;
//            int bottomIndex = width * height - (width / 2);
//            int left = 0, top = 0, right = 0, bottom = 0;

//            int i;
//            for (i = leftIndex; i <= rightIndex; i++)
//            {
//                if (pixels[i] < aRGBThreshold)
//                    break;
//                left++;
//            }
//            for (i = topIndex; i <= bottomIndex; i += width)
//            {
//                if (pixels[i] < aRGBThreshold)
//                    break;
//                top++;
//            }
//            for (i = rightIndex; i >= leftIndex; i--)
//            {
//                if (pixels[i] < aRGBThreshold)
//                    break;
//                right++;
//            }
//            for (i = bottomIndex; i >= topIndex; i -= width)
//            {
//                if (pixels[i] < aRGBThreshold)
//                    break;
//                bottom++;
//            }

//            return new Rect(left, top, width - right - left, height - bottom - top);
//        }

//        public static Rect GetBounded(this WriteableBitmap wb, int aThreshold, int rThreshold, int gThreshold, int bThreshold)
//        {
//            int argbthreshold = (aThreshold << 24) + (rThreshold << 16) + (gThreshold << 8) + bThreshold;
//            return wb.GetBounded(argbthreshold);
//        }
//    }
//}
