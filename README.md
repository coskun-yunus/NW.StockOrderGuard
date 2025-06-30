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
