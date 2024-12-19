using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
	public interface IAppUserService
	{
		public IEnumerable<AppUser> GetAllUsers();
		public AppUser GetUserByIdAsync(string userId);
	}
}
