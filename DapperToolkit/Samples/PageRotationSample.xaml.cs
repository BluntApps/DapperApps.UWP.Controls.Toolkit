using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace DapperToolkit.Samples
{
    public partial class PageRotationSample : PhoneApplicationPage
    {
        public PageRotationSample()
        {
            InitializeComponent();

            OrientationChanged += OnPhoneApplicationPageOrientationChanged;
        }

        private void OnPhoneApplicationPageOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            // Switch the placement of the image panels based on an orientation change.
            if ((e.Orientation & PageOrientation.Portrait) == (PageOrientation.Portrait))
            {
                Grid.SetRow(ImagePanel, 1);
                Grid.SetColumn(ImagePanel, 0);
            }
            // If not in portrait, move image panels to visible row and column.
            else
            {
                Grid.SetRow(ImagePanel, 0);
                Grid.SetColumn(ImagePanel, 1);
            }
        } 
    }
}