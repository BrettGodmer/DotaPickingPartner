using Dota_Picking_Partner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dota_Picking_Partner.Data;

namespace Dota_Picking_Partner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataLayer _dataLayer;

        public HomeController(ILogger<HomeController> logger)
        {
            _dataLayer = new DataLayer();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PickingPartner()
        {
            return View();
        }

        public async Task<IActionResult> CalculateWinrates(HeroSelections heroes)
        {

            HeroCounters heroCounters = new HeroCounters();
            if (!heroes.heroOne.ToString().Equals("n_a")) { heroCounters.heroOneCounters = await _dataLayer.GetHeroCounters(heroes.heroOne.ToString()); };
            if (!heroes.heroTwo.ToString().Equals("n_a")) { heroCounters.heroTwoCounters = await _dataLayer.GetHeroCounters(heroes.heroTwo.ToString()); };
            if (!heroes.heroThree.ToString().Equals("n_a")) { heroCounters.heroThreeCounters = await _dataLayer.GetHeroCounters(heroes.heroThree.ToString()); };
            if (!heroes.heroFour.ToString().Equals("n_a")) { heroCounters.heroFourCounters = await _dataLayer.GetHeroCounters(heroes.heroFour.ToString()); };
            if (!heroes.heroFive.ToString().Equals("n_a")) { heroCounters.heroFiveCounters = await _dataLayer.GetHeroCounters(heroes.heroFive.ToString()); };

            var combinedWinrates = _dataLayer.CombineHeroWinrates(heroCounters);
            
            return View("DisplayWinrates", combinedWinrates);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult AdminAccess()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmAccessAsync(SignIn info)
        {
            if (info.userInput.Equals("ThisIsTheAdminPassword"))
            {
                AdminPanelModel model = new AdminPanelModel(); 
                model.feedback = await _dataLayer.GetAllUserFeedback();
                return View("AdminPanel", model);
            }
            else { return View("Index"); }
        }
        [HttpPost]
        public IActionResult PostUserFeedback(UserFeedback feedback)
        {
            _dataLayer.PostUserFeedback(feedback);
            return View("Index");
        }
         public async Task<IActionResult> RemoveFeedback(string docName)
        {
            await _dataLayer.RemoveUserFeedback(docName);

            AdminPanelModel model = new AdminPanelModel();
            model.feedback = await _dataLayer.GetAllUserFeedback();
            return View("AdminPanel", model);
        }
        public async Task<IActionResult> RunScraper()
        {
            await _dataLayer.RunScraper();
            return View("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}