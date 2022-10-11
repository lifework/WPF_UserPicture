using Prism.Mvvm;
using Reactive.Bindings;
using System.Windows.Media.Imaging;

namespace WPF_UserPicture.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ReactivePropertySlim<BitmapImage> UserPicture { get; set; } = new();

        public MainWindowViewModel()
        {

        }

        private void SetUserPicture()
        {

        }
    }
}
