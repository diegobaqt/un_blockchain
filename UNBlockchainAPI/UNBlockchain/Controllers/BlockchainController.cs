using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UNBlockchain.Models;
using UNBlockchain.Services.Interfaces;

namespace UNBlockchain.Controllers
{
    [RequireHttps]
    [Route("[controller]/[action]")]
    public class BlockchainController : Controller
    {
        private readonly IUNBlockchainService _unBlockchainService;
        private readonly ILogger _logger;

        #region Constructor
        public BlockchainController(
            IUNBlockchainService unBlockchainService, 
            ILogger<BlockchainController> logger)
        {
            _unBlockchainService = unBlockchainService;
            _logger = logger;
        }
        #endregion

        #region GET
        [HttpPost]
        public async Task<IActionResult> GetAccountBalance([FromBody] AddressVm addressVm)
        {
            try
            {
                var result = await _unBlockchainService.GetAccountBalance(addressVm.Address);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.HResult, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return BadRequest("Ha ocurrido un error inesperado.");
            }
        }
        #endregion

        #region POST
        [HttpPost]
        public IActionResult AddAccount()
        {
            try
            {
                var result = _unBlockchainService.CreateAccount();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.HResult, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return BadRequest("Ha ocurrido un error inesperado.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody] TransactionVm transactionVm)
        {
            try
            {
                var result = await _unBlockchainService.Transfer(transactionVm.Secret, transactionVm.ToAddress,
                    transactionVm.Amount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.HResult, ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                return BadRequest("Ha ocurrido un error inesperado.");
            }
        }
        #endregion
    }
}