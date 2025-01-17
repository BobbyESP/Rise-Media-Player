﻿using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Rise.App.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MediaLibraryBasePage : Page
    {
        public static MediaLibraryBasePage Current;
        public MediaLibraryBasePage()
        {
            this.InitializeComponent();
            Current = this;
        }

        private void MediaNav_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var selectedItem = (Microsoft.UI.Xaml.Controls.NavigationViewItem)args.SelectedItem;
            string selectedItemTag = selectedItem.Tag as string;

            switch (selectedItemTag)
            {
                case "Local":
                    MediaFrame.Navigate(typeof(MediaLibraryPage));
                    break;
                case "Services":
                    MediaFrame.Navigate(typeof(OnlineServicesPage));
                    break;
                case "Scanning":
                    MediaFrame.Navigate(typeof(ScanningPage));
                    break;
                default:
                    MediaFrame.Navigate(typeof(ComingSoonPage));
                    break;

            }
        }
    }
}
