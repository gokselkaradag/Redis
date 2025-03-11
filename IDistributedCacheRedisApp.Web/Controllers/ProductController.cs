using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheRedisApp.Web.Controllers;

public class ProductController : Controller
{
    private IDistributedCache _distributedCache;
    
    public ProductController(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    
    // GET
    public async Task<IActionResult> Index()
    {
        DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
        cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(5);
        
        _distributedCache.SetString("name", "John"); //Cache üzerine veri ekler.
        await _distributedCache.SetStringAsync("surname", "Doe");
        return View();
    }
    
    public IActionResult Show()
    {
        string name = _distributedCache.GetString("name"); //Cache üzerindeki veriyi getirir.
        ViewBag.name = name;
        
        return View();
    }

    public IActionResult Remove()
    {
        _distributedCache.Remove("name"); //Cache üzerinde silme işlemi yapar.
        
        return View();
    }
}