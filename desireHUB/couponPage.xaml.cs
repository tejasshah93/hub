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
    public partial class couponPage : PhoneApplicationPage
    {
        string tag;
        public couponPage()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (NavigationContext.QueryString.TryGetValue("tag", out tag))
                displayData(tag);
            //System.Diagnostics.Debug.WriteLine(msg);
            //textBlock1.Text = msg;
        }

        private void displayData(string tag)
        {
            string[] data = tag.Split('^');
            detailTitle.Text = data[0];
            price.Text = data[1];
            discount.Text = data[2];
        }
    }
}