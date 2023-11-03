using EnvCrime.Infrastructure.Shared.Helpers;
using EnvCrime.Models.poco;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnvCrime.Controllers
{
    public class HomeController : Controller
	{		
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public HomeController(UserManager<IdentityUser> userMan, SignInManager<IdentityUser> signInMan)
		{
			userManager = userMan;
			signInManager = signInMan;
		}

		public ViewResult Index()
		{
			var errand = HttpContext.Session.Get<Errand>("NewErrandCitizen");
			if (errand != null)
			{
				return View(errand);
			}
			return View();
		}

		public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginData loginData)
		{
			if (ModelState.IsValid)
			{
                IdentityUser user = await userManager.FindByNameAsync(loginData.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user, loginData.Password, false, false)).Succeeded)
                    {
                        if (await userManager.IsInRoleAsync(user, "Coordinator"))
						{
							return Redirect("/Coordinator/StartCoordinator");
						}
						if (await userManager.IsInRoleAsync(user, "Manager"))
						{
							return Redirect("/Manager/StartManager");
						}
						if (await userManager.IsInRoleAsync(user, "Investigator"))
						{
							return Redirect("/Investigator/StartInvestigator");
						}
                    }
                }
            }
			ModelState.AddModelError("", "Felaktigt användarnamn eller lösenord");
			return View(loginData);
		}

		public async Task<ViewResult> Logout() 
		{
			await signInManager.SignOutAsync();
			return View();
		}
	}
}
