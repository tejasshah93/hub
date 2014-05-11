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


namespace desireHUB
{    
    public partial class eventPage : PhoneApplicationPage
    {
        string tag;
        string[] data;
        public eventPage()
        {
            InitializeComponent();
            data = new string[5];
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //x.Remove(0, 1);
            //string[] data = e.ToString().Split('^');
            //System.Diagnostics.Debug.WriteLine(data[0].ToString());
            if (NavigationContext.QueryString.TryGetValue("tag", out tag))
                displayData(tag);
            //textBlock1.Text = msg;
        }

        private void displayData(string tag)
        {
        
            string[] data = tag.Split('^');
            System.Diagnostics.Debug.WriteLine(data[0]);
            System.Diagnostics.Debug.WriteLine(data[1]);
            System.Diagnostics.Debug.WriteLine(data[2]);
            System.Diagnostics.Debug.WriteLine(data[3]);
            System.Diagnostics.Debug.WriteLine(data[4]);
            detailTitle.Text = data[0];
            startDate.Text = data[1];
            venueName.Text = data[2];
            venueCity.Text = data[3];
        }

 
        //private void loadPage(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    //System.Diagnostics.Debug.WriteLine(data[4]);
        //    // TODO: Add event handler implementation here.
        //    Microsoft.Phone.Tasks.WebBrowserTask wbt = new Microsoft.Phone.Tasks.WebBrowserTask();
        //    wbt.Uri = new Uri("www.google.com", UriKind.Absolute);
        //    wbt.Show();
        //}
    }
}