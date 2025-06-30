## 🧭 Proje Açıklaması

**NW.StockOrderGuard**, modern yazılım mühendisliği ilkelerini temel alarak geliştirilen bir mikroservis tabanlı ürün ve stok yönetim sistemidir. Her bir servis bağımsız olarak tasarlanmış, ayrı bounded context’e sahip olacak şekilde kurgulanmıştır. Sistem; yüksek test edilebilirlik, sürdürülebilirlik ve genişleyebilirlik hedefleriyle inşa edilmiştir.

---

### 🎯 Temel Amaçlar

- Mikroservis mimarisiyle servis bağımsızlığı sağlamak  
- Domain-Driven Design ile iş kurallarını yazılım mimarisiyle bütünleştirmek  
- CQRS + MediatR pattern’leri ile sorumluluk ayrımını yönetmek  
- Hexagonal Architecture ile dış bağımlılıkları soyutlamak  
- Clean Architecture ile kodun yönünü dıştan içe doğru tasarlamak  
- Güçlü güvenlik katmanları ve rate limiting mekanizmaları ile dış saldırılara karşı önlem almak  

---

### 🧱 Kullanılan Mimariler ve Tasarım Kalıpları

| Mimari / Pattern                               | Açıklama |
|------------------------------------------------|----------|
| **Mikroservis Mimarisi**                       | Her servis kendi bounded context’ine sahiptir; bağımsız deploy ve ölçeklenebilirlik sağlanır. |
| **Clean Architecture**                         | Domain → Application → Infrastructure → API katman yapısı ile bağımlılıklar dıştan içe akar. |
| **Hexagonal Architecture (Ports & Adapters)**  | Dış dünya (FakeStore API gibi) adapter ile yalıtılır. İş mantığı sadece port’larla haberleşir. |
| **CQRS (Command Query Responsibility Segregation)** | Komut ve sorgular ayrı işlenir, okuma ve yazma operasyonları ayrıştırılır. |
| **MediatR Pattern**                            | Uygulama içindeki command ve query akışları, loosely-coupled handler'lar üzerinden yönetilir. |
| **SOLID Prensipleri**                          | Sınıflar tek sorumluluk taşır, genişlemeye açık ancak değişime kapalıdır. Katmanlar gevşek bağlıdır. |
| **Adapter Pattern**                            | Dış servisler (örneğin FakeStore API) domain'den soyutlanmış adapter’lar ile entegre edilir. |
| **API Gateway - Ocelot**                       | Tüm servisler tek bir kapıdan yönlendirilir, routing ve güvenlik merkezi olarak yapılır. |
| **Rate Limiting**                              | Ocelot Gateway'de route bazlı istek sınırlandırması uygulanır (örn. 5 istek/dk). |
| **Güvenlik Katmanları**                        | XSS/CSRF farkındalığı, header-based kontrol, stateless authentication altyapısı geliştirmeye hazırdır. |

---
## ⚙️ Çalışma Prensibi ve Sistem Akışı

**NW.StockOrderGuard**, domain odaklı olarak izole edilmiş mikroservislerin, API Gateway (Ocelot) üzerinden yönlendirilmesiyle çalışır. Her bir servis kendi bounded context'i içerisinde sorumludur. Sistem; dış API'den veri çekme, veri işleme, kritik stok tespiti ve sipariş üretme gibi süreçleri domain temelli ve güvenli bir akışla yürütür.

---

### 🧭 Genel Akış
[ UI ] │ ▼ [ API Gateway (Ocelot) ] │ ├─ GET /api/products/sync              → ProductService  → FakeStore API'den ürün çek ├─ GET /api/products                   → ProductService  → Ürün listesi / kritik stok ├─ POST /api/products                  → StockService    → Ürün stok bilgisi güncelle ├─ POST /api/orders/check-and-place   → StockService    → Kritik stoklara sipariş oluştur └─ GET /api/orders                     → StockService    → Siparişleri listele

---

## 🧪 Örnek Senaryo – Sistemin Adım Adım Çalışması

1. **Uygulama Başlatma**  
   - `ProductService`, `StockService`, `ApiGateway` ve varsa `UI` projesi ayağa kaldırılır.

2. **Ürün Senkronizasyonu**  
   - `GET /api/products/sync` endpoint’i çağrılarak FakeStore API’den ürünler sisteme aktarılır.
   - Bu işlem `ProductService` tarafından yapılır ve domain modellerine dönüştürülür.

3. **Ürünlerin Listelenmesi**  
   - `GET /api/products/sync` ya da `GET /api/products` endpoint’i üzerinden ürünler listelenebilir.
   - Listeleme sırasında stok durumu da döner.

4. **Stok Bilgisi Güncelleme**  
   - `POST /api/products` endpoint’i ile belirli ürünlerin stok miktarları güncellenir.
   - Bu işlem `StockService` tarafından gerçekleştirilir.

5. **Kritik Stokların Listelenmesi**  
   - `GET /api/products` endpoint’i, kritik eşiğin altına düşmüş ürünleri de filtreleyerek döner.
   - Sistem, kritik durumdaki stokları domain servisleriyle tespit eder.

6. **Sipariş Oluşturma**  
   - `POST /api/orders/check-and-place` çağrısı ile kritik stoğu olan ürünler için otomatik sipariş oluşturulur.
   - Siparişler `StockService`’in iş kurallarıyla işlenir.

7. **Oluşturulan Siparişlerin Görüntülenmesi**  
   - `GET /api/orders` endpoint’iyle sistemde oluşturulmuş tüm siparişler listelenebilir.

> 🧩 Tüm bu adımlar hem doğrudan API Gateway üzerinden test edilebilir hem de kullanıcı arayüzü üzerinden interaktif şekilde uygulanabilir.  
> 🔗 UI'ya erişmek için: `https://localhost:7000/`

---

> ✅ Bu işleyiş hem domain driven hem de güvenlik bilinçli mimari ile uyumlu ilerler. Her servis yalnızca kendi görevini yerine getirir; sistemin orkestrasyonu ve güvenliği Ocelot API Gateway tarafından merkezi olarak yönetilir.
