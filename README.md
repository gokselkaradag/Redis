# Redis In-Memory Cache Kullanımı

Bu proje, ASP.NET Core ile Redis In-Memory Cache kullanımını göstermektedir. Redis, sık kullanılan verileri bellekte tutarak performansı artıran bir önbellekleme çözümüdür.

📌 Proje İçeriği

* Redis Kurulumu ve Konfigürasyonu

* ASP.NET Core ile Redis Kullanımı

* Veri Okuma ve Yazma İşlemleri

* Cache Süresi Belirleme ve Güncelleme

🛠 Kullanılan Teknolojiler

* ASP.NET Core
* Redis
* Entity Framework
* Docker
  
📌 Nasıl Kullanılır?

* RedisService sınıfı, Redis bağlantısını yönetir ve verileri önbelleğe alır.
* CacheManager ile belirli verileri cache'e ekleyebilir ve süresi dolduğunda güncelleyebilirsin.
* API uç noktaları ile cache mekanizmasını test edebilirsin.
