using Microsoft.AspNetCore.Mvc;
using UserManagement.Application;
using UserManagement.Core;

namespace UserManagement.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View(new User()); 
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            try
            {
                user.Id = Guid.NewGuid();
                _userService.AddUser(user);
                TempData["Success"] = "User created successfully!";
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(user);
            }
        }


        public IActionResult Edit(Guid id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid) return View(user);

            _userService.UpdateUser(user);
            TempData["Success"] = "User updated successfully!";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var user = _userService.GetUserById(id);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _userService.DeleteUser(id);
            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
