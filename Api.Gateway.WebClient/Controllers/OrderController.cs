using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTO;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProxy _orderProxy;
        private readonly ICustomerProxy _customerProxy;
        private readonly ICatalogProxy _catalogProxy;

        public OrderController(ICatalogProxy catalogProxy, ICustomerProxy customerProxy, IOrderProxy orderProxy)
        {
            _catalogProxy = catalogProxy;
            _customerProxy = customerProxy;
            _orderProxy = orderProxy;
        }

        [HttpGet]
        public async Task<DataCollection<OrderDTO>> GetAll(int page, int take)
        {
            var result = await _orderProxy.GetAllAsync(page, take);

            if (result.HasItems)
            {
                var clientIds = result.Items
                    .Select(x => x.ClientId)
                    .GroupBy(g => g)
                    .Select(x => x.Key).ToList();

                var clients = await _customerProxy.GetAllAsync(1, clientIds.Count(), clientIds);

                foreach (var order in result.Items)
                {
                    order.client = clients.Items.Single(x => x.ClientId == order.ClientId);
                }

            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<OrderDTO> Get(int id)
        {
            var result = await _orderProxy.GetAsync(id);

            result.client = await _customerProxy.GetAsync(result.ClientId);

            var productsIds = result.items
                .Select(x => x.ProductId)
                .GroupBy(g => g)
                .Select(x => x.Key).ToList();

            var products = await _catalogProxy.GetAllAsync(1, productsIds.Count(), productsIds);

            foreach(var item in result.items)
            {
                item.Product = products.Items.Single(x => x.ProductId == item.ProductId);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateAsync(command);
            return Ok();
        }
    }
}
