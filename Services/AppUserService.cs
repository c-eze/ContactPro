using ContactPro.Data;
using ContactPro.Models;
using ContactPro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services;

public class AppUserService : IAppUserService
{
	#region Properties
	private readonly ApplicationDbContext _context;
	#endregion

	#region Constructor
	public AppUserService(ApplicationDbContext context)
	{
		_context = context;
	}
	#endregion

	#region Get All Users
	public IEnumerable<AppUser> GetAllUsers()
	{
		return _context.Users;
	}
	#endregion

	#region Get User By Id 
	public AppUser GetUserByIdAsync(string userId)
	{
		return _context.Users.Include(c => c.Contacts)
							 .ThenInclude(c => c.Categories)
							 .FirstOrDefault(u => u.Id == userId);
	}
	#endregion
}
