using Microsoft.AspNetCore.Mvc;
using NW.StockOrderGuard.UI.Web.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NW.StockOrderGuard.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

     

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Products()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");
                var response = await httpClient.GetAsync("api/products");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Ürünler alınamadı.";
                    return View(new List<Models.ProductViewModel>());
                }
                var json = await response.Content.ReadAsStringAsync();
                var products = System.Text.Json.JsonSerializer.Deserialize<List<Models.ProductViewModel>>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(products);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SyncProducts()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");
                var request = new HttpRequestMessage(HttpMethod.Post, "api/products/sync");
                request.Headers.Add("X-XSRF-TOKEN", "gizli-deger");
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["SyncMessage"] = "Ürünler senkronize edilemedi.";
                }
                else
                {
                    TempData["SyncMessage"] = "Ürünler başarıyla senkronize edildi.";
                }
            }
            return RedirectToAction("Products");
        }

        [HttpGet]
        public IActionResult UpdateProductStock()
        {
            return View(new Models.UpdateProductStockViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductStock(Models.UpdateProductStockViewModel model)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");
                var requestObj = new {
                    productName = model.ProductName,
                    thresholdStock = model.ThresholdStock,
                    currentStock = model.CurrentStock
                };
                var request = new HttpRequestMessage(HttpMethod.Post, "api/products");
                request.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(requestObj), System.Text.Encoding.UTF8, "application/json");
                request.Headers.Add("X-XSRF-TOKEN", "gizli-deger");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    model.ResultMessage = "Stok başarıyla güncellendi.";
                }
                else
                {
                    model.ResultMessage = "Stok güncellenemedi.";
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckAndPlaceOrder()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");
                var request = new HttpRequestMessage(HttpMethod.Post, "api/orders/check-and-place");
                request.Headers.Add("X-XSRF-TOKEN", "gizli-deger");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    TempData["OrderMessage"] = "Sipariş kontrol ve otomasyon işlemi başarıyla tamamlandı.";
                }
                else
                {
                    TempData["OrderMessage"] = "Sipariş kontrol ve otomasyon işlemi başarısız.";
                }
            }
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> OrderList()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:5000/");
                var response = await httpClient.GetAsync("api/orders");
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Ürünler alınamadı.";
                    return View(new List<Models.OrderViewModel>());
                }
                var json = await response.Content.ReadAsStringAsync();
                var products = System.Text.Json.JsonSerializer.Deserialize<List<Models.OrderViewModel>>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(products);
            }
        }
    }
}
