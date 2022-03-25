using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTO;
using Api.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogProxy _catalaogProxy;

        public ProductController(ICatalogProxy catalogProxy)
        {
            _catalaogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDTO>> GetAll (int page, int take)
        {
            return await _catalaogProxy.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ProductDTO> Get(int id)
        {
            return await _catalaogProxy.GetAsync(id);
        }
    }
}
