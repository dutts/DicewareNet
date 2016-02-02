using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DicewareNet.Gui.Properties;
using FlickrNet;

namespace DicewareNet.Gui.ImageSource
{
    public class FlickrImageSource : IImageSource
    {
        private readonly Flickr _flickr;

        public FlickrImageSource()
        {
            _flickr = new Flickr(Settings.Default.FlickrAPIKey);
        }

        public async Task<BitmapImage> GetImageForWordAsync(string word)
        {
            Uri wordImageUri;
            var photo = await SearchForPhotoAsync(word);
            var photoResult = photo.FirstOrDefault();
            if (photoResult != null &&
                Uri.TryCreate(photoResult.LargeSquareThumbnailUrl, UriKind.Absolute, out wordImageUri))
            {
                return new BitmapImage(wordImageUri);
            }
            return null;
        }

        private Task<PhotoCollection> SearchForPhotoAsync(string word)
        {
            var t = new TaskCompletionSource<PhotoCollection>();
            var options = new PhotoSearchOptions
            {
                Tags = word,
                PerPage = 1,
                Page = 1,
                Extras = PhotoSearchExtras.ThumbnailUrl
            };
            _flickr.PhotosSearchAsync(options, s => t.TrySetResult(s.Result));
            return t.Task;
        }
    }
}
