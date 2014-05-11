using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using System.IO;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading;
using System.Globalization;

using Microsoft.Phone.Tasks;

namespace desireHUB
{
    public class Tuple
    {
        public int _id { get; set; }
        public string title { get; set; }
        public string telephone { get; set; }
    }

    public class Contact
    {
        public string category { get; set; }
        public List<Tuple> tuples { get; set; }
    }

    public class RootObjectSearch
    {
        //public Id _id { get; set; }
        public List<Contact> contacts { get; set; }
        public string location { get; set; }
    }

    public partial class searchPage : PhoneApplicationPage
    {
        RootObjectSearch jsonObjectSearch;
        string contactJson = "contactJson.json";
        string category;

        public searchPage()
        {
            InitializeComponent();

            // Search Object
            jsonObjectSearch = new RootObjectSearch();
            getContactsJson();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";
            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
                category = msg;
                //System.Diagnostics.Debug.WriteLine(msg);
                //textBlock1.Text = msg;
        }

        private void searchButtonTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            searchData.Items.Clear();
        	// TODO: Add event handler implementation here.
            System.Diagnostics.Debug.WriteLine("Clicking");

            string search = searchBox.Text;
            List<Tuple> results = new List<Tuple>();
            if (category.Equals("all"))
            {
                Contact node = jsonObjectSearch.contacts.Find(delegate(Contact s) { return s.category.Equals(category); });
                List<Tuple> resultList = new List<Tuple>();
                foreach (Tuple tup in node.tuples)
                {
                    resultList.Add(tup);
                }
                results = resultList.FindAll(delegate(Tuple s) { return s.title.Contains(search); });
            }
            else
            {

                Contact node = jsonObjectSearch.contacts.Find(delegate(Contact s) { return s.category.Equals(category); });
                List<Contact> resultList = new List<Contact>();
                foreach (Tuple tup in node.tuples)
                {
                    Contact val = jsonObjectSearch.contacts.Find(delegate(Contact s) { return s.category.Equals(tup.title); });
                    resultList.Add(val);
                }
                List<Tuple> finalTuples = new List<Tuple>();
                foreach (Contact con in resultList)
                {
                    foreach (Tuple tup in con.tuples)
                    {
                        finalTuples.Add(tup);
                    }
                }
                
                results = finalTuples.FindAll(delegate(Tuple s) { return s.title.Contains(search); });
            }
                foreach (Tuple tup in results)
                {
                    //System.Diagnostics.Debug.WriteLine(tup.title);
                    //messagebox.show(eve.title);
                    displayTuple(tup);
                }
        }

        private async void getContactsJson()
        {
            System.Diagnostics.Debug.WriteLine("Getting contacts from file");
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile textFile = await localFolder.GetFileAsync(contactJson);

                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);

                        string jsonContents = textReader.ReadString(textLength);
                        jsonObjectSearch = JsonConvert.DeserializeObject<RootObjectSearch>(jsonContents);
                        //just printing
                        System.Diagnostics.Debug.WriteLine("Getting Contacts...");
                        foreach (Contact con in jsonObjectSearch.contacts)
                        {
                            System.Diagnostics.Debug.WriteLine(con.category);
                            //messagebox.show(eve.title);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string error = "Exception: " + ex.Message;
            }
        }

        // Tuple DIV
        private void displayTuple(Tuple tup)
        {
            var newGrid = new Grid();
            newGrid.Tag = tup.telephone;
            newGrid.Height = 100;
            //dunno
            newGrid.VerticalAlignment = VerticalAlignment.Top;
            newGrid.Width = 456;

            //Image added
            var image = new Image();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = 100;
            image.Width = 100;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Margin = new Thickness(0, 0, 0, 0);
            image.Tag = tup.telephone;
            //image.Tap += callPhone;

            // TitleBlock to be added
            var titleBlock = new TextBlock();
            titleBlock.Text = tup.title;
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.HorizontalAlignment = HorizontalAlignment.Left;
            titleBlock.TextWrapping = TextWrapping.Wrap;
            titleBlock.Height = 60;


            string colorcode = "#FF1BA1E2";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                          (byte)((argb & 0xff0000) >> 0x10),
                          (byte)((argb & 0xff00) >> 8),
                          (byte)(argb & 0xff));

            titleBlock.Foreground = new SolidColorBrush(clr);
            titleBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            titleBlock.FontSize = 20;
            titleBlock.Width = 336;
            titleBlock.Margin = new Thickness(120, 0, 0, 0);


            // StartDateBlock to be added
            var descBlock = new TextBlock();
            descBlock.Text = tup.telephone;
            descBlock.VerticalAlignment = VerticalAlignment.Top;
            descBlock.HorizontalAlignment = HorizontalAlignment.Left;
            descBlock.TextWrapping = TextWrapping.Wrap;
            descBlock.Height = 30;

            descBlock.Foreground = new SolidColorBrush(clr);
            descBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            descBlock.Width = 336;
            descBlock.Margin = new Thickness(120, 60, 0, 0);
            descBlock.Opacity = 0.75;

            
            // Rectangle
            var rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Height = 1;
            rectangle.Margin = new Thickness(0, 95, 0, 0);
            // left stroke
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Width = 456;
            rectangle.Opacity = 0.75;
            rectangle.OpacityMask = new SolidColorBrush(clr);
            rectangle.Fill = new SolidColorBrush(clr);

            newGrid.Children.Add(titleBlock);
            //newGrid.Children.Add(image);
            newGrid.Children.Add(descBlock);
            //newGrid.Children.Add(sourceBlock);
            newGrid.Children.Add(rectangle);

            newGrid.Tap += callPhone;

            searchData.Items.Add(newGrid);
        }

        private void callPhone(object sender, System.Windows.Input.GestureEventArgs e)
        {
            string tag = ((Grid)sender).Tag.ToString();
            PhoneCallTask phoneCallTask = new PhoneCallTask();

            phoneCallTask.PhoneNumber = tag.ToString();
            phoneCallTask.DisplayName = "calling";

            phoneCallTask.Show();
        }
    }
}