using ContactPro.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services.Interfaces
{
	public interface IContactService
	{
		public Task AddNewContactAsync(Contact contact);
		public Task<List<Contact>> GetAllContactsAsync();
		public Task<Contact> GetContactByIdAsync(int id);
		public Task<Contact> GetContactByUserIdAsync(int id, string userId);
		public Task UpdateContactAsync(Contact contact);
		public Task RemoveContactAsync(Contact contact);
	}
}
