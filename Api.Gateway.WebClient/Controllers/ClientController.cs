using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTO;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    public class ClientController : ControllerBase
    {
        private readonly ICustomerProxy _customerProxy;

        public ClientController(ICustomerProxy customerProxy)
        {
            _customerProxy = customerProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDTO>> GetAll(int page, int take)
        {
            return await _customerProxy.GetAllAsync(page, take);
        }

        public async Task<ClientDTO> Get(int id)
        {
            return await _customerProxy.GetAsync(id);
        }

    }
}
