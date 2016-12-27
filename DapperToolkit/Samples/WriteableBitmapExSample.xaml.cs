using DapperApps.Media.Imaging;
using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DapperToolkit.Samples
{
    public partial class WriteableBitmapExSample : PhoneApplicationPage
    {
        public WriteableBitmapExSample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs nea)
        {
            //              Alpha          Red          Green       Blue
            int threshold = (255 << 24) + (255 << 16) + (255 << 8) + 255;

            var bitmap1 = new BitmapImage(new Uri("/Assets/3x3.png", UriKind.Relative));
            bitmap1.ImageOpened +=
                (s, e) =>
                {
                    // 3x3.png
                    WriteableBitmap wbitmap1 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap1.GetBounded(threshold) == Rect.Empty);
                };
            bitmap1.CreateOptions = BitmapCreateOptions.None;

            var bitmap2 = new BitmapImage(new Uri("/Assets/4x4.png", UriKind.Relative));
            bitmap2.ImageOpened +=
                (s, e) =>
                {
                    // 4x4.png
                    WriteableBitmap wbitmap2 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap2.GetBounded(threshold) == new Rect(0, 0, 4, 4));
                };
            bitmap2.CreateOptions = BitmapCreateOptions.None;

            var bitmap3 = new BitmapImage(new Uri("/Assets/5x5.png", UriKind.Relative));
            bitmap3.ImageOpened +=
                (s, e) =>
                {
                    // 5x5.png
                    WriteableBitmap wbitmap3 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap3.GetBounded(threshold) == new Rect(1, 1, 3, 3));
                };
            bitmap3.CreateOptions = BitmapCreateOptions.None;

            var bitmap4 = new BitmapImage(new Uri("/Assets/5x7.png", UriKind.Relative));
            bitmap4.ImageOpened +=
                (s, e) =>
                {
                    // 5x7.png
                    WriteableBitmap wbitmap4 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap4.GetBounded(threshold) == new Rect(1, 1, 4, 5));
                };
            bitmap4.CreateOptions = BitmapCreateOptions.None;

            var bitmap5 = new BitmapImage(new Uri("/Assets/6x5.png", UriKind.Relative));
            bitmap5.ImageOpened +=
                (s, e) =>
                {
                    // 6x5.png
                    WriteableBitmap wbitmap5 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap5.GetBounded(threshold) == new Rect(1, 1, 4, 3));
                };
            bitmap5.CreateOptions = BitmapCreateOptions.None;

            var bitmap6 = new BitmapImage(new Uri("/Assets/7x5.png", UriKind.Relative));
            bitmap6.ImageOpened +=
                (s, e) =>
                {
                    // 7x5.png
                    WriteableBitmap wbitmap6 = new WriteableBitmap((BitmapSource)s);
                    Debug.WriteLine(wbitmap6.GetBounded(threshold) == new Rect(1, 0, 5, 4));
                };
            bitmap6.CreateOptions = BitmapCreateOptions.None;

            base.OnNavigatedTo(nea);
        }
    }
}