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


## 🧭 Endpoint Haritası

| Metod | Endpoint                             | Açıklama                                               | Response Örneği |
|-------|--------------------------------------|---------------------------------------------------------|------------------|
| POST  | `/api/products/sync`                 | Dış API’den ürün verilerini senkronize eder             | `{ }` |
| GET   | `/api/products`                      | Tüm ürünleri ve kritik stok durumlarını listeler        | `[ { } ]` |
| POST  | `/api/products`                      | Ürünlerin stok verilerini günceller                     | `{  }` |
| GET   | `/api/products/{name}`               | Ürün adına göre detaylı ürün bilgisini getirir          | `{  }` |
| GET   | `/api/stock/all`                     | Tüm ürünlerin stok bilgilerini listeler                 | `[ { } ]` |
| GET   | `/api/stock/low-stock`               | Kritik stoktaki ürünleri listeler                       | `[ {  } ]` |
| POST  | `/api/stock/sync`                    | Dış stok verilerini sistemle senkronize eder            | `{ }` |
| POST  | `/api/orders/check-and-place`        | Kritik stoklara otomatik sipariş oluşturur              | `{  }` |
| GET   | `/api/orders`                        | Tüm oluşturulan siparişleri listeler                    | `[ { } ]` |




## 🔌 Fake Store API ile Nasıl Eşleşti?

Sistem, dış kaynak olan Fake Store API'den gelen ürün verisini kendi domain modelleriyle **Hexagonal Architecture (Ports & Adapters)** prensiplerine göre eşleştirecek şekilde yapılandırılmıştır.
Bu yaklaşım sayesinde:
- `FakeStoreClient`, dış API'yi temsil eden adapter katmanında yer alır.
- Böylece dış bağımlılıklar domain mantığından tamamen soyutlanmış olur.
> Bu eşleşme Hexagonal mimari ile yapıldığından, sistemin iç işleyişi dış sistemlerden etkilenmeden çalışır. Gerektiğinde adapter değiştirilebilir, domain dokunulmadan kalır.



# README: Stok Değerlerini Roma Rakamlarına Dönüştürme

Bu proje, HTML tablosundaki kritik ve mevcut stok değerlerini Roma rakamlarına çevirir.

## Nasıl Çalışır?

1.  **`toRoman(num)` Fonksiyonu**: Bir sayıyı Roma rakamına dönüştürür (örn. `100` -> `C`).
2.  **DOM Yükleme Sonrası**: Sayfa yüklendiğinde, tablo satırlarındaki stok değerlerini (`3. ve 4. sütunlar`) yakalar.
3.  Bu sayıları `toRoman()` fonksiyonu ile Roma rakamlarına çevirir ve hücrelere geri yazar.

Amaç, sayısal stok bilgilerini görsel olarak Roma rakamı formatında sunmaktır.

## Uygulanan Kod

```javascript
<script>
    function toRoman(num) {
        if (isNaN(num) || num <= 0) return num; // Added isNaN check for robustness
        const roman = [
            ["M", 1000], ["CM", 900], ["D", 500], ["CD", 400],
            ["C", 100], ["XC", 90], ["L", 50], ["XL", 40],
            ["X", 10], ["IX", 9], ["V", 5], ["IV", 4], ["I", 1]
        ];
        let result = "";
        for (const [letter, n] of roman) {
            while (num >= n) {
                result += letter;
                num -= n;
            }
        }
        return result;
    }

    document.addEventListener("DOMContentLoaded", function () {
        const table = document.querySelector("table.table"); // Assuming table has class "table"
        if (table) {
            for (const row of table.tBodies[0].rows) {
                // Kritik Stok (3. sütun - index 2)
                const kritikCell = row.cells[3]; // Assuming 4th column (index 3) based on original image
                if (kritikCell) {
                    const val = parseInt(kritikCell.textContent, 10);
                    if (!isNaN(val)) { // Ensure it's a valid number before converting
                         kritikCell.textContent = toRoman(val);
                    }
                }

                // Mevcut Stok (4. sütun - index 3)
                const mevcutCell = row.cells[4]; // Assuming 5th column (index 4) based on original image
                if (mevcutCell) {
                    const val = parseInt(mevcutCell.textContent, 10);
                    if (!isNaN(val)) { // Ensure it's a valid number before converting
                        mevcutCell.textContent = toRoman(val);
                    }
                }
            }
        }
    });
</script>
