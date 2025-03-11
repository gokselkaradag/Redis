using IDistributedCacheRedisApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

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
        
        // _distributedCache.SetString("name", "John"); //Cache üzerine veri ekler.
        // await _distributedCache.SetStringAsync("surname", "Doe"); 
        
        Product product = new Product
        {
            Id = 1,
            Name = "Product 1",
            Price = 100
        };
        
        string jsonProduct = JsonConvert.SerializeObject(product); //Product nesnesini JSON formatına çevirir.
        await _distributedCache.SetStringAsync("product:1", jsonProduct, cacheEntryOptions); //Product nesnesini cache üzerine ekler.
        
        return View();
    }
    
    public IActionResult Show()
    {
        // string name = _distributedCache.GetString("name"); //Cache üzerindeki veriyi getirir.
        // ViewBag.name = name;
        
        string jsonProduct = _distributedCache.GetString("product:1"); //Cache üzerindeki veriyi getirir.
        Product product = JsonConvert.DeserializeObject<Product>(jsonProduct); //JSON formatındaki veriyi Product nesnesine çevirir.
        ViewBag.product = product;
        
        return View();
    }

    public IActionResult Remove()
    {
        _distributedCache.Remove("name"); //Cache üzerinde silme işlemi yapar.
        return View();
    }

    public IActionResult ImageCache()
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/ımg/car.jpg"); //Resmin yolunu belirtir.
        byte[] imageByte = System.IO.File.ReadAllBytes(path); //Resmi byte dizisine çevirir.
        _distributedCache.Set("resim", imageByte); //Resmi cache üzerine ekler.
        
        return View();
    }

    public IActionResult ImageUrl()
    {
        byte[] imageByte = _distributedCache.Get("resim"); //Cache üzerindeki resmi getirir.
        return File(imageByte, "image/jpg"); //Resmi ekrana yansıtır.
        
        return View();
    }
}