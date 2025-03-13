using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;

public class ListTypeController : Controller
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;
    private string listKey = "names";
    
    public ListTypeController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(1);
    }
    
    // GET
    public IActionResult Index() //Redis'ten isim listesini alır
    {
        List<string> namesList = new List<string>();
        if (db.KeyExists(listKey))
        {
            db.ListRange(listKey).ToList().ForEach(x =>
            {
                namesList.Add(x.ToString());
            });
        }
        return View(namesList);
    }
    
    [HttpPost]
    public IActionResult Add(string name) //Yeni bir ismi Redis listesine ekler
    {
        db.ListRightPush(listKey, name);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteItem(string name) //Redis listesinden bir ismi siler
    {
        db.ListRemove(listKey, name);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult DeleteFirstItem() //Redis listesinden ilk elemanı siler
    {
        db.ListLeftPop(listKey);
        
        return RedirectToAction("Index");
    }
}