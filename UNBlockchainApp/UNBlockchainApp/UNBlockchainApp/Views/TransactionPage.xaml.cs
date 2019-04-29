using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNBlockchainApp.Services;
using UNBlockchainApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UNBlockchainApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TransactionPage : ContentPage
	{
        private readonly UNBlockchainService _blockchainService;

        public TransactionPage ()
		{
            _blockchainService = new UNBlockchainService();
            InitializeComponent ();

            EntryPrivateKey.Text = Application.Current.Properties["Key"].ToString();
            ButtonSend.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;
                try
                {
                    var privateKey = EntryPrivateKey.Text;
                    var address = EntryAddress.Text;
                    var amount = Convert.ToDecimal(EntryAmount.Text);

                    var transferVm = new TransactionVm
                    {
                        Secret = privateKey,
                        ToAddress = address,
                        Amount = amount
                    };


                    var result = await _blockchainService.Transfer(transferVm);
                    if (result == "No ha sido posible ejecutar la transacción.")
                    {
                        await DisplayAlert("Error", result, "OK");
                    }
                    else
                    {
                        await DisplayAlert("Éxito", result, "OK");
                        var newBalance = await _blockchainService.GetAccountBalance(Application.Current.Properties["Address"].ToString());
                        Application.Current.Properties["Balance"] = "$ " + newBalance;


                        Application.Current.MainPage = new MasterPage();
                    }
                    Loading.IsRunning = false;
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
                    Loading.IsRunning = false;
                }
            };
        }
	}
}