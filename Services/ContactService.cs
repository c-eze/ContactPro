using ContactPro.Data;
using ContactPro.Models;
using ContactPro.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactPro.Services;

public class ContactService : IContactService
{

	#region Properties
	private readonly ApplicationDbContext _context;
	#endregion

	#region Constructor
	public ContactService(ApplicationDbContext context)
	{
		_context = context;
	}
	#endregion

	#region Add New Contact
	public async Task AddNewContactAsync(Contact contact)
	{
		_context.Add(contact);
		await _context.SaveChangesAsync();
	}
	#endregion

	#region Get All Contacts
	public async Task<List<Contact>> GetAllContactsAsync()
	{
		try
		{
			List<Contact> contacts = await _context.Contacts.ToListAsync();
			return contacts;
		}
		catch (Exception)
		{

			throw;
		}
	}
	#endregion

	#region Get Contact By Id
	public async Task<Contact> GetContactByIdAsync(int id)
	{
		try
		{
			return await _context.Contacts.Include(c => c.AppUser)
								 .FirstOrDefaultAsync(m => m.Id == id);
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion

	#region Get Contact By User Id
	public async Task<Contact> GetContactByUserIdAsync(int id, string userId)
	{
		try
		{
			return await _context.Contacts.Where(c => c.Id == id && c.AppUserID == userId)
										  .FirstOrDefaultAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion

	#region Remove Contact
	public async Task RemoveContactAsync(Contact contact)
	{
		try
		{
			_context.Contacts.Remove(contact);
			await _context.SaveChangesAsync();
		}
		catch (Exception)
		{
			throw;
		}
	}
	#endregion

	#region Update Contact
	public async Task UpdateContactAsync(Contact contact)
	{
		try
		{
			_context.Update(contact);
			await _context.SaveChangesAsync();
		}
		catch (Exception)
		{

			throw;
		}
	} 
	#endregion
}
