using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;

public class StringTypeController : Controller
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;
    public StringTypeController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(0); // Redis veritabanı nesnesi
    }
    
    // GET
    public IActionResult Index() // Redis'e veri yaz
    {
        db.StringSet("name", "Goksel Karadag");
        db.StringSet("ziyaretçi", 100);
        return View();
    }
    
    public IActionResult Show()
    {
        var value = db.StringGet("name"); // Redis'ten veriyi çek
        //db.StringIncrement("ziyaretçi",10); // Redis'te ziyaretçi sayısını arttır
        var couunt = db.StringDecrementAsync("ziyaretçi",10).Result; // Redis'te ziyaretçi sayısını azalt
        if (value.HasValue)
        {
            ViewBag.name = value.ToString();
        }
        return View();
    }
}