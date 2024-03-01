using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;

namespace Webshop.Service;

/// <summary>
/// 
/// 
/// </summary>
public class OrderService
{
    private readonly ApiService apiService;
    public OrderService(ApiService apiService) 
    {
        this.apiService = apiService;
    }

    public IList<Order> GetOrders()
    {
        return apiService.GetAndDeserializeAsync<List<Order>>("api/Order").Result;
    }

    public void Create(Order order)
    {
        throw new NotImplementedException();
    }
}
