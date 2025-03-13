using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Services;

public class RedisService
{
    private readonly string _redisHost; // Redis bağlantı adresi
    private readonly string _redisPort; // Redis bağlantı portu
    private ConnectionMultiplexer _redis; // Redis bağlantı nesnesi
    public IDatabase db { get; set; } // Redis veritabanı nesnesi
    
    public RedisService(IConfiguration configuration) // Redis bağlantı bilgilerini al
    {
        _redisHost = configuration["Redis:Host"];
        _redisPort = configuration["Redis:Port"];    
    }
    
    public void Connect() // Redis bağlantısını yap
    {
        var configString = $"{_redisHost}:{_redisPort}";
        
        _redis = ConnectionMultiplexer.Connect(configString);
    }

    public IDatabase GetDb(int db) // Redis veritabanını seç
    {
        return _redis.GetDatabase(db);
    }
}