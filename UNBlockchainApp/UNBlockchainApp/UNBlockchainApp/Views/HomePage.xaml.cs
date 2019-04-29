using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNBlockchainApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();
            WelcomeMessage.Text = "Hola " + Application.Current.Properties["Name"].ToString();
            LabelBalance.Text = Application.Current.Properties["Balance"].ToString();
        }
	}
}