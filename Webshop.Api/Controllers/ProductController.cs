using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webshop.Data;
using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller
{

    private readonly IRepository<Product> _productsRepository;

    private readonly IAuthService authService;

    public ProductController(IRepository<Product> repository, IAuthService authService)
    {
        _productsRepository = repository;
        this.authService = authService;
    }

    // GET: api/Product
    [HttpGet]
    public ActionResult Get([FromQuery] Pagination pagination)
    {
        return Json(_productsRepository.List(pagination.Skip(), pagination.Size).ToList());
    }

    // GET: api/Product/{id}
    [HttpGet("{id}")]
    public ActionResult GetOne(Guid id)
    {
        Product product = _productsRepository.GetById(id);
        if (product == null)
        {
            return Json("");
        }

        return Json(product);
    }

    // GET: api/Product/{id}/Variants
    [HttpGet("{id}/Variants")]
    public ActionResult GetVariantsOfProduct(Guid id)
    {
        List<ProductVariant> result = new();

        foreach (var item in MockData.ProductVariants)
        {
            if (item.ProductId == id)
            {
                result.Add(item);
            }
        }

        return Json(result);
    }

    // GET: api/Product/search/{text}
    [HttpGet("search/{text}")]
    public ActionResult GetSearchProduct(string text)
    {
        List<Product> products = [];

        var found = _productsRepository.List(x => x.Name.Contains(text));
        if (found.Any())
        {
            products.AddRange(found);
        }

        return Json(products);
    }

    // POST: api/Product
    [HttpPost]
    public ActionResult Post([FromBody] Product Product)
    {
        if (string.IsNullOrEmpty(Product.Name) || string.IsNullOrEmpty(Product.Description) || Product.Price <= 0)
        {
            return BadRequest("Missing fields");
        }

        string? token = Request.Headers.Authorization;

        if (token == null) { return BadRequest(); }

        if (token.StartsWith("Bearer"))
        {
            token = token["Bearer ".Length..].Trim();
        }
        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        var user = authService.GetUserFromClaims(jwt.Claims);

        Product.Id = Guid.Empty;
        Product.SellerId = user.Id;

        var result = _productsRepository.Add(Product);

        return Ok(result);
    }

    // DELETE: api/Product
    [HttpDelete]
    public ActionResult Delete([FromBody] Product Product)
    {
        // TODO: Check permission
        _productsRepository.Delete(Product);

        return Ok();
    }

    // PATCH: api/Product
    [HttpPatch("{id}")]
    public ActionResult Patch(Guid id, [FromBody] Product Product)
    {
        var result = _productsRepository.GetById(id);
        if (result == null)
        {
            return BadRequest();
        }
        
        //if (Product)
        result.Name = Product.Name;
        result.Price = Product.Price;
        result.Description = Product.Description;
        result.Stock = Product.Stock;
        result.ImageSource = Product.ImageSource;

        _productsRepository.Edit(result);

        return Ok();
    }

    // GET: api/Product/MyProducts
    [HttpGet("MyProducts")]
    public ActionResult GetMyProducts([FromQuery] Pagination pagination)
    {

        string? token = Request.Headers.Authorization;
        if (token == null) { return BadRequest(); }
        if (token.StartsWith("Bearer"))
        {
            token = token["Bearer ".Length..].Trim();
        }
        var handler = new JwtSecurityTokenHandler();

        JwtSecurityToken jwt = handler.ReadJwtToken(token);

        var user = authService.GetUserFromClaims(jwt.Claims);

        return Json(_productsRepository.List(x => x.SellerId == user.Id));
    }
}
