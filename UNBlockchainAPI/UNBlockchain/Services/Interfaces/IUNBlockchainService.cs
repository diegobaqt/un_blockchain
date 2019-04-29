using System.Threading.Tasks;
using UNBlockchain.Models;

namespace UNBlockchain.Services.Interfaces
{
    public interface IUNBlockchainService
    {
        // GET: Obtain a account balance by address
        Task<AmountVm> GetAccountBalance(string address);

        // POST: Create a new account in a blockchain implementation and return private key
        SecretVm CreateAccount();

        // POST: Make a transaction with sender private key
        Task<ResultVm> Transfer(string privateKey, string toAddress, decimal amount);
    }
}
