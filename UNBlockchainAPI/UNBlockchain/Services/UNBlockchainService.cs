using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using UNBlockchain.Models;
using UNBlockchain.Services.Interfaces;

namespace UNBlockchain.Services
{
    public class UNBlockchainService : IUNBlockchainService
    {
        private readonly IConfiguration _config;

        #region Constructor
        public UNBlockchainService(IConfiguration config)
        {
            _config = config;
        }
        #endregion

        #region GET
        public async Task<AmountVm> GetAccountBalance(string address)
        {
            var web3 = new Web3(_config["Servers:UltraSecretServer"]);
            var balance =
                await web3.Eth.GetBalance.SendRequestAsync(address);
            var etherAmount = Web3.Convert.FromWei(balance.Value);

            return new AmountVm { Value = etherAmount.ToString("#,##0.0000")};
        }
        #endregion

        #region POST
        public SecretVm CreateAccount()
        {
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
            var privateKey = ecKey.GetPrivateKeyAsBytes().ToHex();

            new Web3(_config["Servers:UltraSecretServer"]).Personal.NewAccount.SendRequestAsync(privateKey);

            return new SecretVm {Secret = privateKey};
        }

        public async Task<ResultVm> Transfer(string privateKey, string toAddress, decimal amount)
        {
            var account = new Account(privateKey);
            var web3 = new Web3(account, _config["Servers:UltraSecretServer"]);

            var transaction = await web3.Eth.GetEtherTransferService()
                .TransferEtherAndWaitForReceiptAsync(toAddress, amount);

            return new ResultVm
                {Result = $"Transacción realizada exitosamente con hash {transaction.TransactionHash}."};
        }
        #endregion
    }
}
