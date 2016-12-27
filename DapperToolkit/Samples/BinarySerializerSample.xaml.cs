using DapperApps.Phone.Serialization;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;

namespace DapperToolkit.Samples
{
    public partial class BinarySerializerSample : PhoneApplicationPage
    {
        public BinarySerializerSample()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Show current IsoStore.
                    StoreResults.Text = EnumerateStore(store, "", new StringBuilder()).ToString();

                    var uri = new Uri("Assets/test1.jpg", UriKind.Relative);
                    var resourceInfo = Application.GetResourceStream(uri);
                    var bmp = new BitmapImage();
                    bmp.SetSource(resourceInfo.Stream);

                    // Attempt to serialize our testclass.
                    var serializer = new BinarySerializer(typeof(TestClass));

                    //using (var stream = store.OpenFile("Serialized", FileMode.Create, FileAccess.ReadWrite))
                    using (var stream = new MemoryStream())
                    {
                        var test = new TestClass
                        {
                            Name = "Cache",
                            Strings = new List<string> { "Hello", "World", "From", "BinarySerializer" },
                            Image = new WriteableBitmap(bmp)
                        };

                        serializer.Serialize(stream, test);

                        test = null;
                        StoreResults.Text = EnumerateStore(store, "", new StringBuilder()).ToString();

                        stream.Seek(0, SeekOrigin.Begin);

                        test = (TestClass)serializer.Deserialize(stream);

                        Image.Source = test.Image;
                    }
                }
            }
            base.OnNavigatedTo(e);
        }

        private StringBuilder EnumerateStore(IsolatedStorageFile store, string path, StringBuilder result)
        {
            var directories = store.GetDirectoryNames(Path.Combine(path, "*"));
            result.Append("Directories in: \'").Append(path).Append('\'').Append(' ').Append(directories.Length.ToString()).Append('\n');
            foreach (var directory in directories)
            {
                result.Append('\t').Append(directory).Append(' ');
            }

            var files = store.GetFileNames(Path.Combine(path, "*.*"));
            result.Append('\n').Append("Files in: \'").Append(path).Append('\'').Append(' ').Append(files.Length.ToString()).Append('\n');
            foreach (var file in files)
            {
                result.Append('\t').Append(file).Append(' ');
            }

            foreach (var directory in directories)
            {
                EnumerateStore(store, Path.Combine(path, directory), result.Append('\n'));
            }
            return result;
        }
    }

    public class TestClass
    {
        [DataMemberAttribute]
        public string Name { get; set; }

        [DataMemberAttribute]
        public IList<string> Strings { get; set; }

        [DataMemberAttribute]
        public WriteableBitmap Image { get; set; }
    }
}