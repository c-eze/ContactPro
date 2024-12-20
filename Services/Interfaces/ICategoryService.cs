using ContactPro.Models;

namespace ContactPro.Services.Interfaces
{
	public interface ICategoryService
	{
		public Task AddNewCategoryAsync(Category category);
		public Task<List<Category>> GetAllCategoriesAsync();
		public Task<List<Category>> GetCategoriesByUserIdAsync(string userId);
		public Task<Category> GetCategoryByUserIdAsync(string userId, int id);
		public Task UpdateCategoryAsync(string userId, Category category);
		public Task RemoveCategoryAsync(Category category);
	}
}
