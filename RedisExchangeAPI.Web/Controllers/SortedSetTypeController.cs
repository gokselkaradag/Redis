using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers;

public class SortedSetTypeController : Controller
{
    private readonly RedisService _redisService;
    private readonly IDatabase db;
    private string listKey = "sortednames";
    
    public SortedSetTypeController(RedisService redisService)
    {
        _redisService = redisService;
        db = _redisService.GetDb(3);
    }
    
    // GET
    public IActionResult Index()
    {
        HashSet<string> sortedNames = new HashSet<string>();
        db.SortedSetScan(listKey).ToList().ForEach(x => sortedNames.Add(x.ToString()));
        return View(sortedNames);
    }
    
    [HttpPost]
    public IActionResult Add(string name, double score)
    {
        db.SortedSetAdd(listKey,name, score); // SortedSet'e eleman eklemek için SortedSetAdd metodu kullanılır.
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> DeleteItem(string name)
    {
        await db.SortedSetRemoveAsync(listKey, name);
        return RedirectToAction("Index");
    }
}