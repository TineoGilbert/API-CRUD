using API_STORE.Data;
using API_STORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_STORE.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController: ControllerBase
    {
        public async Task <ActionResult<List<MProducts>>> Get()
        {
            var func = new DProducts();
            var list = await func.showProducts();
            return list;
        }

        [HttpPost]
        public async Task Post([FromBody] MProducts param)
        {
            var func = new DProducts();
            await func.insertProduct(param);
        }

        [HttpPut("{id}")]
        public async Task <ActionResult> Put(int id, [FromBody] MProducts param)
        {
            var func = new DProducts();
            param.id = id;
            await func.editProduct(param);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var func = new DProducts();
            var param = new MProducts();
            param.id = id;
            await func.deleteProduct(param);
            return NoContent();
        }
    }
}
