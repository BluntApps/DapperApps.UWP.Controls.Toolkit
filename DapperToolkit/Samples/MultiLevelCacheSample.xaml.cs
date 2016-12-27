using DapperApps.Phone.Collections;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Windows.Storage;

namespace DapperToolkit.Samples
{
    public partial class MultiLevelCacheSample : PhoneApplicationPage
    {
        private MultiLevelCache<Person> _cache;

        public static StringBuilder folderContents;
        public static string FOLDER_PREFIX = "\\";
        public static int PADDING_FACTOR = 3;
        public static char SPACE = ' ';

        public MultiLevelCacheSample()
        {
            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.New)
            //{
            //    var results = await EnumerateFilesAndFolders(ApplicationData.Current.LocalFolder);
            //    Debug.WriteLine(results);

            //    var cache = new MultiLevelCache<Person>();

            //    results = await EnumerateFilesAndFolders(ApplicationData.Current.LocalFolder);
            //    Debug.WriteLine(results);

            //    if (IsolatedStorageSettings.ApplicationSettings.Contains("dummy"))
            //    {
            //        Person fran = cache["fran"];
            //        PersonName.Text = fran.Name;
            //        PersonLastName.Text = fran.LastName;
            //        PersonFavePic.Source = fran.FavoritePicture;
            //        PersonHobbies.ItemsSource = fran.Hobbies;
            //    }
            //    else
            //    {
            //        IsolatedStorageSettings.ApplicationSettings.Add("dummy", false);
            //        var fran = new Person { Name = "Francisco", LastName = "Aguilera", Age = 24, Hobbies = new List<string> { "Computers", "Sailing", "Soccer" } };
            //        var bfInfo = Application.GetResourceStream(new Uri("Assets/test2.jpg", UriKind.Relative));
            //        var bfPic = new BitmapImage();
            //        bfPic.SetSource(bfInfo.Stream);
            //        fran.FavoritePicture = new WriteableBitmap(bfPic);
            //        cache.Add("fran", fran);
            //        IsolatedStorageSettings.ApplicationSettings.Save();
            //    }
            //}
            base.OnNavigatedTo(e);
        }

        // Begin recursive enumeration of files and folders.
        public static async Task<string> EnumerateFilesAndFolders(StorageFolder rootFolder)
        {
            // Initialize StringBuilder to contain output.
            folderContents = new StringBuilder();
            folderContents.AppendLine(FOLDER_PREFIX + rootFolder.Name);

            await ListFilesInFolder(rootFolder, 1);

            return folderContents.ToString();
        }

        // Continue recursive enumeration of files and folders.
        private static async Task ListFilesInFolder(StorageFolder folder, int indentationLevel)
        {
            string indentationPadding = String.Empty.PadRight(indentationLevel * PADDING_FACTOR, SPACE);

            // Get the subfolders in the current folder.
            var foldersInFolder = await folder.GetFoldersAsync();
            // Increase the indentation level of the output.
            int childIndentationLevel = indentationLevel + 1;
            // For each subfolder, call this method again recursively.
            foreach (StorageFolder currentChildFolder in foldersInFolder)
            {
                folderContents.AppendLine(indentationPadding + FOLDER_PREFIX + currentChildFolder.Name);
                await ListFilesInFolder(currentChildFolder, childIndentationLevel);
            }

            // Get the files in the current folder.
            var filesInFolder = await folder.GetFilesAsync();
            foreach (StorageFile currentFile in filesInFolder)
            {
                folderContents.AppendLine(indentationPadding + currentFile.Name);
            }
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (null == _cache)
            {
                _cache = new MultiLevelCache<Person>();
            }

            var person = new Person { Name = NameTextBox.Text, Age = AgeTextBox.Text };
            if (!_cache.ContainsKey(person.Name))
            {
                _cache.Add(person.Name, person);
            }
            else
            {
                MessageBox.Show("Cannot add a person with the same name!");
            }
            People.ItemsSource = _cache.Values;
        }

        private void ReadFromStorage_Click(object sender, RoutedEventArgs e)
        {
            _cache = new MultiLevelCache<Person>();
            People.ItemsSource = _cache.Values;
        }
    }

    public class Person
    {
        [DataMemberAttribute]
        public string Name { get; set; }

        [DataMemberAttribute]
        public string LastName { get; set; }

        [DataMemberAttribute]
        public WriteableBitmap FavoritePicture { get; set; }

        [DataMemberAttribute]
        public string Age { get; set; }

        [DataMemberAttribute]
        public List<string> Hobbies { get; set; }
    }
}