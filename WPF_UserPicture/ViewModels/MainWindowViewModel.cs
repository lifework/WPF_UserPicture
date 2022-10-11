using ImTools;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using Windows.Networking.NetworkOperators;
using Windows.Storage.Streams;
using Windows.System;

namespace WPF_UserPicture.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {

        public ReactivePropertySlim<string?> AccountName { get; set; } = new();
        public ReactivePropertySlim<BitmapImage> UserPicture { get; set; } = new();

        public MainWindowViewModel()
        {
            SetUserPicture();
        }

        private async void SetUserPicture()
        {
            var user = (await User.FindAllAsync(UserType.LocalUser, UserAuthenticationStatus.LocallyAuthenticated)).FirstOrDefault();
            if (user == null) { return; }

            AccountName.Value = await user.GetPropertyAsync(KnownUserProperties.AccountName) as string;


            // C# converting UWP BitmapImage to WPF BitmapImage
            // https://stackoverflow.com/questions/50596735/c-sharp-converting-uwp-bitmapimage-to-wpf-bitmapimage
            IRandomAccessStreamReference streamReference = await user.GetPictureAsync(UserPictureSize.Size208x208);
            if (streamReference != null)
            {
                var bitmap = new BitmapImage();
                using var randomAccessStream = await streamReference.OpenReadAsync();
                using var stream = randomAccessStream.AsStream();

                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                UserPicture.Value = bitmap;
            }
        }
    }
}
