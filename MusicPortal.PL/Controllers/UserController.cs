using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
//using MusicPortal.DAL.Entities;

namespace MusicPortal.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService<UserDTO> userService;
        public UserController(IUserService<UserDTO> service)
        {
            this.userService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Login,Password")] UserDTO user)
        {
            if (!ModelState.IsValid) return View(user);

            if (user.Login.ToLower() == "admin")
            {
                ModelState.AddModelError("Login", "Логин 'admin' не подходит.");
            }

            if (!await userService.IsUnique(user.Login))
            {
                ModelState.AddModelError("Login", "Этот логин уже занят.");
                return View(user);
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await userService.Create(user);
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserDTO model)
        {
            var user = await userService.Login(model);

            if (user != null)
            {
                var option = new CookieOptions { Expires = DateTime.Now.AddDays(10) };
                Response.Cookies.Append("login", user.Login, option);
                return RedirectToAction("Index", "Music");
            }

            ModelState.AddModelError("", "Неверный логин или пароль");
            return View(model);
        }

        public ActionResult Logout()
        {
            Response.Cookies.Delete("login");
            return RedirectToAction("Index", "Music");
        }

    }
}
