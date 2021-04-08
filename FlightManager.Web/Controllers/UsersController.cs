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


        /// <summary>
        /// Взима всички потребители и ги страницира.
        /// </summary>
        /// <param name="model"></param>
        /// <returns> Изгледа на страницата и всички потребители подредени в таблица </returns>
        //GET: User/AllUsers
        public async Task<IActionResult> AllUsers(UserIndexViewModel model)
        {
            var ReturnModel = await userServices.ReturnPages(model);
            ViewBag.ReturnedUsers = ReturnModel.Items;
            ViewBag.ReturnedUsersPagers = ReturnModel.Pager.PagesCount;
            ViewBag.ReturnedUsersPagersCurrentPage = ReturnModel.Pager.CurrentPage;

            return View();
        }

        /// <summary>
        /// Гет заявка за добавяне на потребители.
        /// </summary>
        /// <returns> Изгледа на страницата </returns>
        //GET: User/AddUser
        public IActionResult AddUser()
        {
            return View();
        }

        /// <summary>
        /// Пост заявка за добавяне на потребители.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        /// <returns> Изгледа на страницата </returns>
        //POST: User/AddUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user, string Password)
        {
            if(ModelState.IsValid)
            {
                userServices.Create(user, Password);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "One or more fields are empty.");
                return View();
            }
            
            return View();
        }

        /// <summary>
        /// Гет заявка за промяна на даден потребител.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Изгледа на страницата и потребителя който ще се променя, а ако няма такъв потребител връща HTTP 404 </returns>
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

        /// <summary>
        /// Пост заявка за промяна на дадения потребител.
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Връща обратно към AllUsers изгледа </returns>
        //Post: User/EditUser/4
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(UserEditViewModel user)
        {
            if (ModelState.IsValid)
            {
                userServices.Update(user);
            }
            return RedirectToAction(nameof(AllUsers));
        }

        /// <summary>
        /// Гет заявка за изтриване на дадения потребител.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> Връща обратно към AllUsers изгледа </returns>
        //Get: User/Delete/4
        public async Task<IActionResult> Delete(string Id)
        {
            userServices.Delete(Id);
            return RedirectToAction(nameof(AllUsers));
        }
    }
}
