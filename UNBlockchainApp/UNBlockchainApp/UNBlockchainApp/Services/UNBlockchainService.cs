using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UNBlockchainApp.ViewModels;

namespace UNBlockchainApp.Services
{
    public class UNBlockchainService
    {
        private const string _httpsUrl = "https://unblockchain.azurewebsites.net/";
        private readonly HttpClient _client;

        public UNBlockchainService()
        {
            _client = new HttpClient { BaseAddress = new Uri(_httpsUrl) };
        }

        public async Task<string> CreateAccount()
        {
            var response = await _client.PostAsync(_client.BaseAddress + "Blockchain/AddAccount", null);
            if (response.IsSuccessStatusCode)
            {
                var json = JsonConvert.SerializeObject("");
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var secretVm = JsonConvert.DeserializeObject<SecretVm>(response.Content.ReadAsStringAsync().Result);
                return secretVm.Secret;
            }
            else
            {
                return "No ha sido posible crear la cuenta.";
            }
        }

        public async Task<string> GetAccountBalance(string address)
        {
            var json = JsonConvert.SerializeObject(new AddressVm { Address = address });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Blockchain/GetAccountBalance", content);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<AmountVm>(response.Content.ReadAsStringAsync().Result);
                return result.Value;
            }
            else
            {
                return "No ha sido posible crear la cuenta.";
            }
        }

        public async Task<string> Transfer(TransactionVm transactionVm)
        {
            var json = JsonConvert.SerializeObject(transactionVm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("Blockchain/Transfer", content);
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ResultVm>(response.Content.ReadAsStringAsync().Result);
                return result.Result;
            }
            else
            {
                return "No ha sido posible ejecutar la transacción.";
            }
        }
    }
}
