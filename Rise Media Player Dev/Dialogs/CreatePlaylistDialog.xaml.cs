﻿using Rise.App.Common;
using Rise.App.ViewModels;
using Rise.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Rise.App.Dialogs
{
    public sealed partial class CreatePlaylistDialog : ContentDialog
    {
        private Uri _imagePath = new Uri("ms-appx:///Assets/DefaultPlaylistIcon.png");

        public CreatePlaylistDialog()
        {
            InitializeComponent();
        }

        #region Events/Methods

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string title = string.IsNullOrWhiteSpace(TitleTextBox.Text) ? "Untitled" : TitleTextBox.Text;
            string description = string.IsNullOrWhiteSpace(DescriptionTextBox.Text) ? "No description." : DescriptionTextBox.Text;

            PlaylistViewModel plViewModel = new PlaylistViewModel
            {
                Title = title,
                Description = description,
                Icon = _imagePath.OriginalString,
                Duration = "0",
                SongsCollection = new ObservableCollection<SongViewModel>()
            };

            await plViewModel.SaveAsync();
            
            Hide();
        }

        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args) => Hide();

        private void Image_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            UseCustomImageButton.Visibility = Visibility.Visible;
        }

        private void Image_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            UseCustomImageButton.Visibility = Visibility.Collapsed;
        }

        private async void UseCustomImageButton_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();

            if (file != null)
            {
                // Get file thumbnail and make a PNG out of it.
                StorageItemThumbnail thumbnail = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 200);

                await FileHelpers.SaveBitmapFromThumbnailAsync(thumbnail, $@"playlist-{file.Name}.png");

                thumbnail.Dispose();
                _imagePath = new Uri($@"ms-appdata:///local/playlist-{file.Name}.png");
            }

            PreviewImage.Source = new BitmapImage(_imagePath);
        }

        #endregion

    }
}