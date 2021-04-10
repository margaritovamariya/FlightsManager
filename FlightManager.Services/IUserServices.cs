using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    /// <summary>
    /// UserServices Interface
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// Seeds RolesTable
        /// </summary>
        public void SeedRoles();
        /// <summary>
        /// Seeds User and UserRoles Table
        /// </summary>
        public void SeedUserRoles();

        /// <summary>
        /// Calls Create method from UserServices
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        public void Create(User user, string Password);

        /// <summary>
        /// Calls Update method from UserServices
        /// </summary>
        /// <param name="user"></param>
        public void Update(UserEditViewModel user);

        /// <summary>
        /// Calls Delete method from UserServices
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(string Id);

        /// <summary>
        /// Calls ReturnPages method from UserServices
        /// </summary>
        /// <param name="model"></param>
        /// <returns> List of UserViewModel </returns>
        public Task<UserIndexViewModel> ReturnPages(UserIndexViewModel model);

        /// <summary>
        /// Calls FindAsync method from UserServices
        /// </summary>
        /// <param name="id"></param>
        /// <returns> User by given id </returns>
        public Task<User> FindAsync(string id);

    }
}
