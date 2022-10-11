using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Linq;
using System.Windows.Media.Imaging;
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
        }
    }
}
