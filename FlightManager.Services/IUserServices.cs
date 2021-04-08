using FlightManager.Models;
using FlightManager.Services.Models.OutputModels;
using System.Threading.Tasks;

namespace FlightManager.Services
{
    public interface IUserServices
    {
        public void SeedRoles();
        public void SeedUserRoles();
        public void Create(User user, string Password);

        public void Update(UserEditViewModel user);

        public void Delete(string Id);

        public Task<UserIndexViewModel> ReturnPages(UserIndexViewModel model);

        public Task<User> FindAsync(string id);

    }
}
