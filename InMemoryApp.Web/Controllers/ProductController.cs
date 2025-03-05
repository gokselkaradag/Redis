using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        
        // GET: ProductController
        public ActionResult Index()
        {
            // 1.Yol
            /*if (String.IsNullOrEmpty(_memoryCache.Get<string>("zaman")))
            {
                _memoryCache.Set<string>("Zaman", DateTime.Now.ToString());
            }*/
            
            // 2.Yol
            
            if (!_memoryCache.TryGetValue("date", out string zamancache))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                //options.AbsoluteExpiration = DateTime.Now.AddSeconds(30); // 30 saniye sonra silinir.
                options.SlidingExpiration = TimeSpan.FromSeconds(10); // 10 saniye boyunca işlem yapmazsa silinir.
                _memoryCache.Set<string>("date", DateTime.Now.ToString(), options);
                //_memoryCache.Set("Date", DateTime.Now.ToString(), options);
            }

            
            //_memoryCache.Set<string>("Zaman", DateTime.Now.ToString());
            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.Remove("zaman"); //Bu key sahip olan datayı siler.
            
            /*_memoryCache.GetOrCreate<string>("zaman", entry =>
            {
                return DateTime.Now.ToString();
            });*/ 
            //Eğer key yoksa oluşturur.
            
            
            _memoryCache.TryGetValue("date", out string zamancache);
            ViewBag.Zaman = zamancache; 
            
            //ViewBag.zaman = _memoryCache.Get<string>("Date"); //Zaman keyine sahip olan datayı getirir.
            return View();
        }

    }
}
