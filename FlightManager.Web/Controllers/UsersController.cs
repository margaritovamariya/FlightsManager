using FlightManager.Models;
using FlightManager.Services;
using FlightManager.Services.Models.OutputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlightManager.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices userServices;

        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        //GET: User/AllUsers
        public async Task<IActionResult> AllUsers(UserIndexViewModel model)
        {
            var ReturnModel = userServices.ReturnPages(model);
            ViewBag.ReturnedUsers = ReturnModel.Result.Items;
            ViewBag.ReturnedUsersPagers = ReturnModel.Result.Pager.PagesCount;
            ViewBag.ReturnedUsersPagersCurrentPage = ReturnModel.Result.Pager.CurrentPage;

            return View();
        }

        //GET: User/AddUser
        public IActionResult AddUser()
        {
            return View();
        }

        //POST: User/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user, string Password)
        {
            userServices.Create(user, Password);
            return View();
        }

        //Get: User/EditUser/4
        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ReturnedUserEditView = await userServices.FindAsync(id);
            if (ReturnedUserEditView == null)
            {
                return NotFound();
            }

            ViewBag.UserForEdit = ReturnedUserEditView;

            return View();
        }

        //Post: User/EditUser/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                userServices.Update(user);
            }
            return RedirectToAction(nameof(AllUsers));
        }

        //Get: User/Delete/4
        public async Task<IActionResult> Delete(string Id)
        {
            userServices.Delete(Id);
            return RedirectToAction(nameof(AllUsers));
        }
    }
}
