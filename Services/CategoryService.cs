using ContactPro.Data;
using ContactPro.Models;
using ContactPro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services
{
	public class CategoryService : ICategoryService
	{
		#region Properties
		private readonly ApplicationDbContext _context;
		#endregion

		#region Constructor
		public CategoryService(ApplicationDbContext context)
		{
			_context = context;
		}
		#endregion

		#region Add New Category 
		public async Task AddNewCategoryAsync(Category category)
		{
			try
			{
				_context.Add(category);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion

		#region Get All Categories
		public async Task<List<Category>> GetAllCategoriesAsync()
		{
			try
			{
				return await _context.Categories.ToListAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion

		#region Get Categories By UserId
		public async Task<List<Category>> GetCategoriesByUserIdAsync(string userId)
		{
			try
			{
				return await _context.Categories.Where(c => c.AppUserId == userId)
												.Include(c => c.AppUser)
												.Include(c => c.Contacts)
												.ToListAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion

		#region Get Category By UserId
		public async Task<Category> GetCategoryByUserIdAsync(string userId, int id)
		{
			try
			{
				return await _context.Categories
									 .Include(c => c.Contacts)
									 .FirstOrDefaultAsync(c => c.Id == id && c.AppUserId == userId);
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion

		#region Remove Category
		public async Task RemoveCategoryAsync(Category category)
		{
			try
			{
				_context.Categories.Remove(category);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		}
		#endregion

		#region Update Category
		public async Task UpdateCategoryAsync(string userId, Category category)
		{
			try
			{
				category.AppUserId = userId;
				_context.Update(category);
				await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

				throw;
			}
		} 
		#endregion
	}
}
