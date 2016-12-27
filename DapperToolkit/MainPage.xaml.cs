using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DapperToolkit.Resources;

namespace DapperToolkit
{
    public partial class MainPage : PhoneApplicationPage
    {
        private SampleItem[] _samples;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            InitializeData();
            samplesLLS.ItemsSource = _samples;
        }

        private void InitializeData()
        {
            // These items show up sorted by the second parameter, not the name of the file
            _samples = new SampleItem[] {
                new SampleItem("/Samples/ScrollEventSample.xaml", "scrollevents", "scrolling threshold events" ),
                new SampleItem("/Samples/PageRotationSample.xaml", "rotationanimations", "animates an element with orientation changes"),
                new SampleItem("/Samples/PTZImageSample.xaml", "ptzimage", "a pinch-to-zoomable image"),
                new SampleItem("/Samples/ImagePreviewSample.xaml", "imagepreview", "shows a preview of an image"),
                new SampleItem("/Samples/BinarySerializerSample.xaml", "BinarySerializer", "Serialize objects to binary data"),
                new SampleItem("/Samples/MultiLevelCacheSample.xaml", "MultiLevelCache", "Maintains a cache of objects in memory and storage")
            };
        }
    }

    public class SampleItem
    {
        public SampleItem(string url, string header, string description)
        {
            Url = url;
            Header = header;
            Description = description;
        }

        public string Url { get; private set; }
        public string Header { get; private set; }
        public string Description { get; private set; }
    }
}