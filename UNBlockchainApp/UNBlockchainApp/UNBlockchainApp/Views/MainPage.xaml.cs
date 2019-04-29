using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNBlockchainApp.Services;
using UNBlockchainApp.Views;
using Xamarin.Forms;

namespace UNBlockchainApp
{
    public partial class MainPage : ContentPage
    {
        private readonly UNBlockchainService _blockchainService;

        public MainPage()
        {
            _blockchainService = new UNBlockchainService();
            InitializeComponent();

            ButtonNewAccount.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;
                try
                {
                    var result = await _blockchainService.CreateAccount();
                    if (result == "No ha sido posible crear la cuenta.")
                    {
                        await DisplayAlert("Error", result, "OK");
                    }
                    else
                    {
                        await DisplayAlert("Éxito", $"Cuenta creada exitosamente con llave privada { result }. Conéctate a MetaMask para conocer tu dirección", "OK");
                        EntryKey.Text = result;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
                }
                
                Loading.IsRunning = false;
            };

            ButtonLogin.Clicked += async (sender, args) =>
            {
                Loading.IsRunning = true;

                Application.Current.Properties["Name"] = EntryName.Text;
                Application.Current.Properties["Address"] = EntryAddress.Text;
                Application.Current.Properties["Key"] = EntryKey.Text;
                Application.Current.Properties["Balance"] = "$0,0000";

                try
                {
                    var result = await _blockchainService.GetAccountBalance(EntryAddress.Text);
                    if (result == "No ha sido posible obtener el saldo.")
                    {
                        await DisplayAlert("Error", result, "OK");
                    } else
                    {
                        Application.Current.Properties["Balance"] = "$ " + result;
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error inesperado", "OK");
                }

                Application.Current.MainPage = new MasterPage();
            };
        }

        
    }
}
