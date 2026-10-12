using HolyBackend.Core.Interfaces;
using HolyBackend.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace HolyBackend.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPrayerRepository _prayerRepository;

        public HomeController(IPrayerRepository prayerRepository)
        {
            _prayerRepository = prayerRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new PrayerRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Index(PrayerRequest request)
        {
            if (ModelState.IsValid)
            {
                await _prayerRepository.AddPrayerAsync(request);
                TempData["SuccessMessage"] = "¬аша молитва усп≥шно дос€гла сервер≥в. јм≥нь.";
                return RedirectToAction("Index");
            }
            return View(request);
        }
    }
}