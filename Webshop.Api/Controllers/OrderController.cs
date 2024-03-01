using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Webshop.Data;
using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController(IRepository<Order> orderRepository) : Controller
{
    private readonly IRepository<Order> orderRepository = orderRepository;

    // GET: api/Order
    [HttpGet]
    public ActionResult Get([FromQuery] Pagination pagination)
    {
        return Json(orderRepository.List(pagination.Skip(), pagination.Size).ToList());
    }
}
