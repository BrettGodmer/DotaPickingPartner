using Dota_Picking_Partner.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dota_Picking_Partner.Data;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Dota_Picking_Partner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataLayer _dataLayer;
        private FirebaseAuthClient _firebaseAuthClient;
        private FirebaseAuthConfig _firebaseAuthConfig;

        public HomeController(ILogger<HomeController> logger)
        {
            _dataLayer = new DataLayer();
            _logger = logger;
            _firebaseAuthConfig = new FirebaseAuthConfig() {
                ApiKey = "AIzaSyBYnKu8p-vnYq_y6BnUKUpKBICLgTs4sQI",
                AuthDomain = "dota-picking-partner.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                    {
                        new GoogleProvider().AddScopes("email"),
                        new EmailProvider()
                    }
            };
            _firebaseAuthClient = new FirebaseAuthClient(_firebaseAuthConfig);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                var auth = await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(model.userEmail, model.password);
                var email = auth.User.Info.Email;
                return RedirectToAction("Index", new {email = email});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Invalid email or password");
                return View(model);
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (model.password.Equals(model.confirmPassword))
            {
                try
                {
                    var auth = await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(model.userEmail, model.password);
                    var email = auth.User.Info.Email;
                    return RedirectToAction("Index", new { email = email });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to create account. Please try again later.");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Passwords do not match");
                return View(model);
            }

        }


        public IActionResult Index(string? email)
        {
            IndexViewModel model = new IndexViewModel();
            if (email is null)
            {
                model.isGuest = true;
            }
            else
            {
                model.isGuest = false;
                model.email = email;
            }
             return View("Index", model);
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
        public IActionResult ContactUs(string userEmail)
        {
            UserFeedback model = new UserFeedback { ContactInfo = userEmail };
            return View("ContactUs", model);
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
            else {
                ModelState.AddModelError("", "Incorrect Password");
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult PostUserFeedback(UserFeedback feedback, string contactInfo)
        {
            feedback.ContactInfo = contactInfo;
            _dataLayer.PostUserFeedback(feedback);
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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