using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ContactPro.Data;
using ContactPro.Models;
using ContactPro.Models.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using ContactPro.Enums;
using ContactPro.Services.Interfaces;

namespace ContactPro.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailService;
        private readonly ICategoryService _categoryService;

		public CategoriesController(UserManager<AppUser> userManager,
									IEmailSender emailService,
									ICategoryService categoryService)
		{
			_userManager = userManager;
			_emailService = emailService;
			_categoryService = categoryService;
		}

		// GET: Categories
		[Authorize]
        public async Task<IActionResult> Index(string swalMessage = null)
        {
            ViewData["SwalMessage"] = swalMessage;

            string appUserId = _userManager.GetUserId(User);

            var categories = await _categoryService.GetCategoriesByUserIdAsync(appUserId);
            
            return View(categories);
        }

		// GET: Contacts/Details/5
		[Authorize]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			string appUserId = _userManager.GetUserId(User);

			Category category = await _categoryService.GetCategoryByUserIdAsync(appUserId, id.Value);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[Authorize]
		public async Task<IActionResult> EmailCategory(int id)
		{
			string appUserId = _userManager.GetUserId(User);

			Category category = await _categoryService.GetCategoryByUserIdAsync(appUserId, id);

			List<string> emails = category.Contacts.Select(c => c.Email).ToList();

			EmailData emailData = new EmailData()
			{
				GroupName = category.Name,
				EmailAddress = String.Join(";", emails),
				Subject = $"Group Message: {category.Name}"
			};

			EmailCategoryViewModel model = new EmailCategoryViewModel()
			{
				Contacts = category.Contacts.ToList(),
				EmailData = emailData
			};

			return PartialView("_EmailCategory", model);
		}

		[Authorize]
        [HttpPost]
        public async Task<IActionResult> EmailCategory (EmailCategoryViewModel ecvm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _emailService.SendEmailAsync(ecvm.EmailData.EmailAddress, ecvm.EmailData.Subject, ecvm.EmailData.Body);
                    return RedirectToAction("Index", "Categories", new { swalMessage = "Success: Email Sent" });
                }
                catch
                {
                    return RedirectToAction("Index", "Categories", new { swalMessage = "Error: Email Send Failed!" });
                    throw;
                }
            }

            return View(ecvm);
        }

        // GET: Categories/Create
        [Authorize]
        public IActionResult Create()
        {
            Category model = new Category(); 

			return PartialView("_CreateGroup", model);
		}

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUserId,Name")] Category category)
        {
            ModelState.Remove("AppUserId");

            if (ModelState.IsValid)
            {
                string appUserId = _userManager.GetUserId(User);
                category.AppUserId = appUserId;

                await _categoryService.AddNewCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            } 
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string appUserId = _userManager.GetUserId(User);
            var category = await _categoryService.GetCategoryByUserIdAsync(appUserId, id.Value);

			if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUserId,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string appUserId = _userManager.GetUserId(User);
                    await _categoryService.UpdateCategoryAsync(appUserId, category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            } 
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string appUserId = _userManager.GetUserId(User);

            var category = await _categoryService.GetCategoryByUserIdAsync(appUserId, id.Value);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string appUserId = _userManager.GetUserId(User);

			var category = await _categoryService.GetCategoryByUserIdAsync(appUserId, id);

			if (category != null)
            {
                await _categoryService.RemoveCategoryAsync(category);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
          return (await _categoryService.GetAllCategoriesAsync()).Any(e => e.Id == id);
        }
    }
}
