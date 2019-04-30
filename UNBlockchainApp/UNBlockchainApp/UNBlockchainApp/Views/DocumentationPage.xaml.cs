using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNBlockchainApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DocumentationPage : ContentPage
	{
		public DocumentationPage ()
		{
			InitializeComponent ();
            var browser = new WebView
            {
                Source = "https://github.com/diegobaqt/un_blockchain/blob/master/README.md"
            };

            Content = browser;
        }
	}
}