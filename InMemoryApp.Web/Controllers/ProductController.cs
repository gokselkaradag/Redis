using InMemoryApp.Web.Models;
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
                
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(30); // 30 saniye sonra silinir.
               
                options.SlidingExpiration = TimeSpan.FromSeconds(10); // 10 saniye boyunca işlem yapmazsa silinir.
               
                options.Priority = CacheItemPriority.Normal; // Dataların önceliklerinin belirlenmesi.
               
                options.RegisterPostEvictionCallback(((key, value, reason, state) =>
                {
                    _memoryCache.Set("callback", $"{key} - {value} -> Reason: {reason}");
                    // Önbellek girdisi kaldırıldığında, bu geri çağırma tetiklenir ve kaldırılma nedeni ile yeni bir önbellek girdisi oluşturur.
                }));
               
                
                _memoryCache.Set<string>("date", DateTime.Now.ToString(), options);
                //_memoryCache.Set("Date", DateTime.Now.ToString(), options);
            }
            
            //_memoryCache.Set<string>("Zaman", DateTime.Now.ToString());
            
            Product p = new Product{Id = 1, Name = "Kalem", Price = 100};
            _memoryCache.Set<Product>("product:1", p); // product:1 keyine sahip olan datayı set eder.
            
            return View();
        }

        public IActionResult Show()
        {
            //_memoryCache.Remove("zaman"); //Bu key sahip olan datayı siler.
            
            /*_memoryCache.GetOrCreate<string>("zaman", entry =>
            {
                return DateTime.Now.ToString();
                
                Eğer key yoksa oluşturur.
            });*/ 
            
            
            
            _memoryCache.TryGetValue("date", out string zamancache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.Zaman = zamancache; 
            ViewBag.callback = callback; 
            
            ViewBag.Product = _memoryCache.Get<Product>("product:1");
            
            //ViewBag.zaman = _memoryCache.Get<string>("Date"); //Zaman keyine sahip olan datayı getirir.
            return View();
        }

    }
}