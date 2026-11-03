using Microsoft.AspNetCore.Mvc;
using StokYonetim.Application.DTOs;
using StokYonetim.Application.Interfaces;

namespace StokYonetim.Web.Controllers
{
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }



        public async Task<IActionResult> Index()
        {
            var urunler = await _urunService.TumUrunleriGetirAsync();
            return View(urunler);
        }
        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ekle(UrunCreateDto yeniUrunDto)
        {
            if (ModelState.IsValid)
            {
                await _urunService.UrunEkleAsync(yeniUrunDto);
                return RedirectToAction(nameof(Index));
            }
            return View(yeniUrunDto);
        }


    }
}
