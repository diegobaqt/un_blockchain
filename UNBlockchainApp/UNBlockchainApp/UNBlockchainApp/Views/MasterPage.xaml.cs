using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNBlockchainApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : TabbedPage
    {
        public MasterPage ()
        {
            InitializeComponent();
            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.SetIsSwipePagingEnabled(this, false);

            Children.Add(new HomePage { Icon = "ic_home" });
            Children.Add(new TransactionPage { Icon = "ic_transaction" });
            Children.Add(new DocumentationPage { Icon = "ic_doc" });
        }
    }
}