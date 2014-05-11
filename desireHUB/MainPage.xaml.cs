using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using desireHUB.Resources;

//used
using System.IO;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Threading;
using System.Globalization;

namespace desireHUB
{
    public class Event
    {
        public string title { get; set; }
        public string url { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string category { get; set; }
        public string status { get; set; }
        public string created { get; set; }
        public string organizer_name { get; set; }
        public string organizer_description { get; set; }
        public string organizer_long_description { get; set; }
        public string venue_name { get; set; }
        public string venue_address { get; set; }
        public string venue_address_2 { get; set; }
        public string venue_postal_code { get; set; }
        public string venue_city { get; set; }
        public string venue_region { get; set; }
        public string venue_country { get; set; }
        public int timestamp { get; set; }
    }

    public class News
    {
        public string title { get; set; }
        public string link { get; set; }
        public string pubDate { get; set; }
        public int timestamp { get; set; }
        public string description { get; set; }
    }

    public class Deal
    {
        public string url { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string saving { get; set; }
        public string image { get; set; }
        public int timestamp { get; set; }
    }

    public class RootObject
    {
        public List<Event> events { get; set; }
        public List<News> news { get; set; }
        public List<Deal> deals { get; set; }
        public int events_timestamp { get; set; }
        public int news_timestamp { get; set; }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // News list we will serialize to and from
        //List<News> m_NewsList = null;

        RootObject jsonObject;
        string dataJson = "dataJson.json";
        string contactJson = "contactJson.json";

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Showing Images Code
            Uri uri = new Uri("http://4.bp.blogspot.com/-f-kOhBdHiiY/T3rgdG6rvmI/AAAAAAAABGc/oxNa9FE89T4/s1600/laughing-lol-crazy-l.png", UriKind.Absolute);
            profileImage.Source = new BitmapImage(uri);

            // Feeds Object
            jsonObject = new RootObject();
            

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;

            // Fetching Feeds from Server
            FetchFeeds("Hyderabad", "20");

            // Set Json
            //setJson();

            
            // Contacts Data Loaded

            // Fetch the contacts
            FetchContacts();

            int j = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                j = j + 1;
            }
            // Get the fetched Feeds Json
            getJson();

            // Load the contacts

            calldisplay();
            //displayData();
        }

        private void calldisplay()
        {
            displayData();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}


        //Json Parsing
        private void parseJson(string jsonString)
        {
            jsonObject = JsonConvert.DeserializeObject<RootObject>(jsonString);
            foreach (Event eve in jsonObject.events)
            {
                System.Diagnostics.Debug.WriteLine(eve.title);
                //messagebox.show(eve.title);
            }
        }



        // DISPLAY UI

        // Event DIV
        private void displayEvent(Event eve)
        {
            string tag = eve.title + "^" + eve.start_date + "^" + eve.venue_name + "^" + eve.venue_city + "^" + eve.url;
            var newGrid = new Grid();
            newGrid.Tag = tag;
            newGrid.Height = 240;
            //dunno
            newGrid.VerticalAlignment = VerticalAlignment.Top;
            newGrid.Width = 456;

            // Image added

            

            var image = new Image();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = 180;
            image.Width = 180;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Margin = new Thickness(0, 0, 0, 45);

            //var imageBrush = new ImageBrush();
            image.Source = new BitmapImage(new Uri(@"\Assets\event.jpg", UriKind.Relative));
            //imageBrush.Stretch = Stretch.None;

            

            //Uri uri = new Uri("Assets/event.jpg", UriKind.Absolute);
            //image.Source = new BitmapImage(uri);
            
            // TitleBlock to be added
            var titleBlock = new TextBlock();
            titleBlock.Text = eve.title;
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.HorizontalAlignment = HorizontalAlignment.Left;
            titleBlock.TextWrapping = TextWrapping.Wrap;
            titleBlock.Height = 54;


            string colorcode = "#FF1BA1E2";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                          (byte)((argb & 0xff0000) >> 0x10),
                          (byte)((argb & 0xff00) >> 8),
                          (byte)(argb & 0xff));

            titleBlock.Foreground = new SolidColorBrush(clr);
            titleBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            titleBlock.Width = 261;
            titleBlock.Margin = new Thickness(195,0,0,0);

            
            // StartDateBlock to be added
            var startDateBlock = new TextBlock();
            startDateBlock.Text = eve.start_date;
            startDateBlock.VerticalAlignment = VerticalAlignment.Top;
            startDateBlock.HorizontalAlignment = HorizontalAlignment.Left;
            startDateBlock.TextWrapping = TextWrapping.Wrap;
            startDateBlock.Height = 27;

            startDateBlock.Foreground = new SolidColorBrush(clr);
            startDateBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            startDateBlock.Width = 261;
            startDateBlock.Margin = new Thickness(195, 54, 0, 0);
            startDateBlock.Opacity = 0.75;

            // EventVenueBlock to be added
            var eventVenueBlock = new TextBlock();
            eventVenueBlock.Text = eve.venue_name;
            eventVenueBlock.VerticalAlignment = VerticalAlignment.Top;
            eventVenueBlock.HorizontalAlignment = HorizontalAlignment.Left;
            eventVenueBlock.TextWrapping = TextWrapping.Wrap;
            eventVenueBlock.Height = 27;

            eventVenueBlock.Foreground = new SolidColorBrush(clr);
            eventVenueBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            eventVenueBlock.Width = 261;
            eventVenueBlock.Margin = new Thickness(195, 81, 0, 0);
            eventVenueBlock.Opacity = 0.75;

            // EventVenueCityBlock to be added
            var eventVenueCityBlock = new TextBlock();
            eventVenueCityBlock.Text = eve.venue_city;
            eventVenueCityBlock.VerticalAlignment = VerticalAlignment.Top;
            eventVenueCityBlock.HorizontalAlignment = HorizontalAlignment.Left;
            eventVenueCityBlock.TextWrapping = TextWrapping.Wrap;
            eventVenueCityBlock.Height = 27;

            eventVenueCityBlock.Foreground = new SolidColorBrush(clr);
            eventVenueCityBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            eventVenueCityBlock.Width = 261;
            eventVenueCityBlock.Margin = new Thickness(195, 108, 0, 0);
            eventVenueCityBlock.Opacity = 0.75;
            
            // Button
            var button = new Button();
            button.Content = "RSVP";
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.Margin = new Thickness(239, 137, 68, 0);
            button.RenderTransformOrigin = new Point(0.421, -0.044);
            button.Width = 136;
            button.FontSize = 22.667;
            button.Height = 72;
            button.Foreground = new SolidColorBrush(clr);
            button.BorderBrush = new SolidColorBrush(clr);
            
            // Rectangle
            var rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Height = 1;
            rectangle.Margin = new Thickness(0, 224, 0, 0);
            // left stroke
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Width = 456;
            rectangle.Opacity = 0.75;
            rectangle.OpacityMask = new SolidColorBrush(clr);
            rectangle.Fill = new SolidColorBrush(clr);

            newGrid.Children.Add(image);
            newGrid.Children.Add(titleBlock);
            newGrid.Children.Add(startDateBlock);
            newGrid.Children.Add(eventVenueBlock);
            newGrid.Children.Add(eventVenueCityBlock);
            newGrid.Children.Add(button);
            newGrid.Children.Add(rectangle);

            newGrid.Tap += testCode;

            feedsData.Items.Add(newGrid);
        }

        private void testCode(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var tag = ((Grid)sender).Tag.ToString();
            var height = ((Grid)sender).Height.ToString();
            if(height.Equals("240"))
            {
                NavigationService.Navigate(new Uri("/eventPage.xaml?tag="+tag, UriKind.Relative));
            }
            if(height.Equals("230"))
            {
                NavigationService.Navigate(new Uri("/couponPage.xaml?tag="+tag, UriKind.Relative));
            }
            if(height.Equals("220"))
            {
                NavigationService.Navigate(new Uri("/newsPage.xaml?msg=train"+tag, UriKind.Relative));
            }
            System.Diagnostics.Debug.WriteLine("Tapped YAYAYAY!!");
            System.Diagnostics.Debug.WriteLine(tag);
        }

        // Deal DIV
        private void displayCoupon(Deal adeal)
        {
            string tag = adeal.title + "^" + adeal.price + "^" + adeal.saving;
            var newGrid = new Grid();
            newGrid.Tag = tag;
            newGrid.Height = 230;
            //dunno
            newGrid.VerticalAlignment = VerticalAlignment.Top;
            newGrid.Width = 456;

            // Image added
            var image = new Image();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = 180;
            image.Width = 180;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Margin = new Thickness(0, 0, 0, 45);
            image.Source = new BitmapImage(new Uri(@"\Assets\coupon.jpg", UriKind.Relative));

//            adeal.image = adeal.image.Replace(@"\", "");
//            System.Diagnostics.Debug.WriteLine("Image URL");
//            System.Diagnostics.Debug.WriteLine(adeal.image);
//            Uri uri = new Uri(adeal.image, UriKind.Absolute);
//           image.Source = new BitmapImage(uri);

            // TitleBlock to be added
            var titleBlock = new TextBlock();
            titleBlock.Text = adeal.title;
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.HorizontalAlignment = HorizontalAlignment.Left;
            titleBlock.TextWrapping = TextWrapping.Wrap;
            titleBlock.Height = 80;


            string colorcode = "#FF1BA1E2";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                          (byte)((argb & 0xff0000) >> 0x10),
                          (byte)((argb & 0xff00) >> 8),
                          (byte)(argb & 0xff));

            titleBlock.Foreground = new SolidColorBrush(clr);
            titleBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            titleBlock.Width = 261;
            titleBlock.Margin = new Thickness(195, 0, 0, 0);


            // StartDateBlock to be added
            var priceBlock = new TextBlock();
            priceBlock.Text = adeal.price;
            priceBlock.VerticalAlignment = VerticalAlignment.Top;
            priceBlock.HorizontalAlignment = HorizontalAlignment.Left;
            priceBlock.TextWrapping = TextWrapping.Wrap;
            priceBlock.Height = 27;

            priceBlock.Foreground = new SolidColorBrush(clr);
            priceBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            priceBlock.Width = 261;
            priceBlock.Margin = new Thickness(195, 80, 0, 0);
            priceBlock.Opacity = 0.75;

            // EventVenueBlock to be added
            var savingBlock = new TextBlock();
            savingBlock.Text = adeal.saving;
            savingBlock.VerticalAlignment = VerticalAlignment.Top;
            savingBlock.HorizontalAlignment = HorizontalAlignment.Left;
            savingBlock.TextWrapping = TextWrapping.Wrap;
            savingBlock.Height = 27;

            savingBlock.Foreground = new SolidColorBrush(clr);
            savingBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            savingBlock.Width = 261;
            savingBlock.Margin = new Thickness(195, 107, 0, 0);
            savingBlock.Opacity = 0.75;

            // Button
            var button = new Button();
            button.Content = "Claim";
            button.HorizontalAlignment = HorizontalAlignment.Right;
            button.Margin = new Thickness(239, 130, 68, 0);
            button.RenderTransformOrigin = new Point(0.421, -0.044);
            button.Width = 136;
            button.FontSize = 22.667;
            button.Height = 72;
            button.Foreground = new SolidColorBrush(clr);
            button.BorderBrush = new SolidColorBrush(clr);

            // Rectangle
            var rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Height = 1;
            rectangle.Margin = new Thickness(0, 224, 0, 0);
            // left stroke
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Width = 456;
            rectangle.Opacity = 0.75;
            rectangle.OpacityMask = new SolidColorBrush(clr);
            rectangle.Fill = new SolidColorBrush(clr);

            newGrid.Children.Add(image);
            newGrid.Children.Add(titleBlock);
            newGrid.Children.Add(priceBlock);
            newGrid.Children.Add(savingBlock);
            newGrid.Children.Add(button);
            newGrid.Children.Add(rectangle);

            newGrid.Tap += testCode;

            feedsData.Items.Add(newGrid);
        }

        // News DIV
        private void displayNews(News anews)
        {
            string tag = anews.title + "^" + anews.link;
            var newGrid = new Grid();
            newGrid.Tag = tag;
            newGrid.Height = 220;
            //dunno
            newGrid.VerticalAlignment = VerticalAlignment.Top;
            newGrid.Width = 456;

            // Image added
            var image = new Image();
            image.HorizontalAlignment = HorizontalAlignment.Left;
            image.Height = 100;
            image.Width = 100;
            image.VerticalAlignment = VerticalAlignment.Top;
            image.Margin = new Thickness(0, 70, 0, 0);
            image.Source = new BitmapImage(new Uri(@"\Assets\news.jpg", UriKind.Relative));

            // TitleBlock to be added
            var titleBlock = new TextBlock();
            titleBlock.Text = anews.title;
            titleBlock.VerticalAlignment = VerticalAlignment.Top;
            titleBlock.HorizontalAlignment = HorizontalAlignment.Left;
            titleBlock.TextWrapping = TextWrapping.Wrap;
            titleBlock.Height = 50;


            string colorcode = "#FF1BA1E2";
            int argb = Int32.Parse(colorcode.Replace("#", ""), NumberStyles.HexNumber);
            Color clr = Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                          (byte)((argb & 0xff0000) >> 0x10),
                          (byte)((argb & 0xff00) >> 8),
                          (byte)(argb & 0xff));

            titleBlock.Foreground = new SolidColorBrush(clr);
            titleBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            titleBlock.Width = 456;
            titleBlock.Margin = new Thickness(0, 0, 0, 0);


            // StartDateBlock to be added
            var descBlock = new TextBlock();
            descBlock.Text = anews.description;
            descBlock.VerticalAlignment = VerticalAlignment.Top;
            descBlock.HorizontalAlignment = HorizontalAlignment.Left;
            descBlock.TextWrapping = TextWrapping.Wrap;
            descBlock.Height = 80;

            descBlock.Foreground = new SolidColorBrush(clr);
            descBlock.FontFamily = new FontFamily("Segoe WP Semibold");
            descBlock.Width = 336;
            descBlock.Margin = new Thickness(120, 96, 0, 0);
            descBlock.Opacity = 0.75;

            // Rectangle
            var rectangle = new System.Windows.Shapes.Rectangle();
            rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            rectangle.Height = 1;
            rectangle.Margin = new Thickness(0, 224, 0, 0);
            // left stroke
            rectangle.VerticalAlignment = VerticalAlignment.Top;
            rectangle.Width = 456;
            rectangle.Opacity = 0.75;
            rectangle.OpacityMask = new SolidColorBrush(clr);
            rectangle.Fill = new SolidColorBrush(clr);

            newGrid.Children.Add(titleBlock);
            newGrid.Children.Add(image);
            newGrid.Children.Add(descBlock);
            newGrid.Children.Add(rectangle);

            newGrid.Tap += testCode;

            feedsData.Items.Add(newGrid);
        }


        private void displayData()
        {   
            try
            {
                System.Diagnostics.Debug.WriteLine("try");
                foreach (Event eve in jsonObject.events)
                {
                    System.Diagnostics.Debug.WriteLine(eve.title);
                    //messagebox.show(eve.title);

                    displayEvent(eve);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                foreach (Deal adeal in jsonObject.deals)
                {
                    System.Diagnostics.Debug.WriteLine(adeal.title);
                    //messagebox.show(eve.title);

                    displayCoupon(adeal);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                foreach (News anews in jsonObject.news)
                {
                    System.Diagnostics.Debug.WriteLine(anews.title);
                    //messagebox.show(eve.title);

                    displayNews(anews);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void setJson()
        {
            string jsonContents = JsonConvert.SerializeObject(jsonObject);
            System.Diagnostics.Debug.WriteLine("String");
            System.Diagnostics.Debug.WriteLine(jsonContents);
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(dataJson, CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (DataWriter textWriter = new DataWriter(textStream))
                {
                    textWriter.WriteString(jsonContents);
                    await textWriter.StoreAsync();
                }
            }
        }

        private async void getJson()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                StorageFile textFile = await localFolder.GetFileAsync(dataJson);

                using (IRandomAccessStream textStream = await textFile.OpenReadAsync())
                {
                    using (DataReader textReader = new DataReader(textStream))
                    {
                        uint textLength = (uint)textStream.Size;
                        await textReader.LoadAsync(textLength);

                        string jsonContents = textReader.ReadString(textLength);
                        jsonObject = JsonConvert.DeserializeObject<RootObject>(jsonContents);
                        //just printing
                        System.Diagnostics.Debug.WriteLine("Getting...");
                        foreach (Event eve in jsonObject.events)
                        {
                            System.Diagnostics.Debug.WriteLine(eve.title);
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


        //------------ Feeds Section --------------//
        
        private void FetchFeeds(string region, string age)
        {
            System.Diagnostics.Debug.WriteLine("Fetch");
            // retrieve an avatar image from the Web
            string avatarUri = "http://54.201.62.76:8080/data?id=1&regID=ewr&city_eventsTimestamp=0&city_newsTimestamp=0&city_dealsTimestamp=0&state_newsTimestamp=0&country_newsTimestamp=0&region=" + region + "&age=" + age;
            HttpWebRequest request =
                (HttpWebRequest)HttpWebRequest.Create(avatarUri);
            request.BeginGetResponse(GetAvatarImageCallback, request);
        }

        async void GetAvatarImageCallback(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    string results;
                    using (StreamReader httpwebStreamReader = new StreamReader(response.GetResponseStream()))
                    {
                        results = httpwebStreamReader.ReadToEnd();
                        //TextBlockResults.Text = results; //-- on another thread!
                        //Dispatcher.BeginInvoke(() => TextBlockResults.Text = results);
                    }
                    //parseJson(results);


                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile textFile = await localFolder.CreateFileAsync(dataJson, CreationCollisionOption.ReplaceExisting);

                    using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        using (DataWriter textWriter = new DataWriter(textStream))
                        {
                            textWriter.WriteString(results);
                            await textWriter.StoreAsync();
                        }
                    }

                    //setJson(results);

                    System.Diagnostics.Debug.WriteLine(results);
                    //Console.Write("%s",results);
                    //avatarImg = Texture2D.FromStream(
                    //  graphics.GraphicsDevice,
                    //response.GetResponseStream());
                }
                catch (WebException e)
                {
                    string gamerTag = "Gamertag not found.";
                    return;
                }
            }
        }


        // Search Page Code

        //------------ Search Section --------------//
        
        private void FetchContacts()
        {
            System.Diagnostics.Debug.WriteLine("Fetch Contacts");
            // retrieve an avatar image from the Web
            string avatarUri = "http://54.201.62.76/justdialHyderabad.json";
            HttpWebRequest request =
                (HttpWebRequest)HttpWebRequest.Create(avatarUri);
            request.BeginGetResponse(GetContactsCallback, request);
        }

        async void GetContactsCallback(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    string results;
                    using (StreamReader httpwebStreamReader = new StreamReader(response.GetResponseStream()))
                    {
                        results = httpwebStreamReader.ReadToEnd();
                        //TextBlockResults.Text = results; //-- on another thread!
                        //Dispatcher.BeginInvoke(() => TextBlockResults.Text = results);
                    }
                    //parseJson(results);


                    StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                    StorageFile textFile = await localFolder.CreateFileAsync(contactJson, CreationCollisionOption.ReplaceExisting);

                    using (IRandomAccessStream textStream = await textFile.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        using (DataWriter textWriter = new DataWriter(textStream))
                        {
                            textWriter.WriteString(results);
                            await textWriter.StoreAsync();
                        }
                    }

                    //setJson(results);

                    System.Diagnostics.Debug.WriteLine(results);
                    //Console.Write("%s",results);
                    //avatarImg = Texture2D.FromStream(
                    //  graphics.GraphicsDevice,
                    //response.GetResponseStream());
                }
                catch (WebException e)
                {
                    string gamerTag = "Gamertag not found.";
                    return;
                }
            }
        }

        

        private void doctorTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=Doctors", UriKind.Relative));
        }

        private void hotelsTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=Hotels", UriKind.Relative));
        }

        private void utilitiesTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=Essentials", UriKind.Relative));
        }

        private void taxisTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=Taxis", UriKind.Relative));
        }

        private void trainTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=train", UriKind.Relative));
        }

        private void exploreTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            NavigationService.Navigate(new Uri("/searchPage.xaml?msg=all", UriKind.Relative));
        }

        private void updateFeed(object sender, System.Windows.Input.GestureEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            string region = location.Text;
            string age = DateOfBirth.Text;
            FetchFeeds(region, age);
            // Get the fetched Feeds Json

            int j = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                j = j + 1;
            }

            //jsonObject = null;

            getJson();

            // Load the contacts
            feedsData.Items.Clear();
            calldisplay();
            
        }

        
    }
}