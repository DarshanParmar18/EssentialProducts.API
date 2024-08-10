using EssentialProducts.API.ViewModel.Create;
using EssentialProducts.API.ViewModel.Get;
using EssentialProducts.API.ViewModel.Update;
using EssentialProducts.Core;
using EssentialProducts.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EssentialProducts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        // GET: api/<ProductController>
        [HttpGet("",Name ="GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProductViewModel>),StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var products = await productService.GetProductsAsync();
            var models = products.Select(product=> new ProductViewModel()
            {
                Id = product.Id,
                AvailableSince = product.AvailableSince,
                CategoryId = product.CategoryId,
                Description = product.Description,
                IsActive = product.IsActive,
                Name = product.Name,
                Price = product.Price
            }).ToList();
            return Ok(models);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}",Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var model = new ProductViewModel()
            {
                Id = product.Id,
                AvailableSince = product.AvailableSince,
                CategoryId = product.CategoryId,
                Description = product.Description,
                IsActive = product.IsActive,
                Name = product.Name,
                Price = product.Price
            };
            return Ok(model);
        }

        // POST api/<ProductController>
        [HttpPost("",Name ="CreateProducts")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ModelStateDictionary),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateProduct createProduct)
        {
            var entityToAdd = new Product()
            {
                Name = createProduct.Name,
                AvailableSince = createProduct.AvailableSince,
                CategoryId = createProduct.CategoryId,
                CreatedDate= DateTime.Now,
                Description = createProduct.Description,
                IsActive = createProduct.IsActive,
                Price = createProduct.Price,
            };
            entityToAdd.ProductOwner = new ProductOwner() { OwnerADObjId="Admin",OwnerName="Admin"};

            var createdProduct = await productService.CreateProductAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new {id = createdProduct.Id});

        } 

        // PUT api/<ProductController>/5
        [HttpPut("{id}",Name ="UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProduct updateProduct)
        {
            var getProduct = await productService.GetProductAsync(id);

            if (getProduct == null)
            {
                return NotFound(); 
            }

            getProduct.Name = updateProduct.Name;
            getProduct.AvailableSince = updateProduct.AvailableSince;
            getProduct.CategoryId = updateProduct.CategoryId;
            getProduct.ModifiedDate = DateTime.Now;
            getProduct.ModifiedBy = "Admin";
            getProduct.Description = updateProduct.Description;
            getProduct.IsActive = updateProduct.IsActive;
            getProduct.Price = updateProduct.Price;

            await productService.UpdateProductAsync(getProduct);
             
            return Ok(updateProduct);            
        }
        
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}",Name ="DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var IsSuccess = await productService.DeleteProductAsync(id);
            return Ok(IsSuccess);
        }
    }
}
