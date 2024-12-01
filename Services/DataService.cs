using ContactPro.Data;
using ContactPro.Models;
using Microsoft.AspNetCore.Identity;
using NuGet.Packaging;

namespace ContactPro.Services
{
    public class DataService
    {
        private List<Contact> _contacts;
        private List<Category> _categories;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public DataService(ApplicationDbContext dbContext,
                           UserManager<AppUser> userManager,
                           IConfiguration configuration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task ManageDataAsync()
        {
            //Task 2: Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedUsersAsync()
        {
            //If there are already users in the system, do nothing. 
            if (_dbContext.Users.Any())
            {
                return;
            }

            //Step 1: Create new instances of User
            var demoAdmin = new AppUser()
            {
                Email = "demoadmin@mailinator.com",
                UserName = "demoadmin@mailinator.com",
                FirstName = "Mary",
                LastName = "DemoAdmin",
                PhoneNumber = "(800)555-1212",
                EmailConfirmed = true
            }; 

            var demoUser = new AppUser()
            {
                Email = "demouser@mailinator.com",
                UserName = "demouser@mailinator.com",
                FirstName = "Joe",
                LastName = "DemoUser",
                PhoneNumber = "(800)555-1212",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined by demoAdmin and demoUser
            string password = Environment.GetEnvironmentVariable("UserPassword") ?? _configuration.GetSection("User")["Password"];
            await _userManager.CreateAsync(demoAdmin, password);
            await _userManager.CreateAsync(demoUser, password);

            await _dbContext.SaveChangesAsync();

            List<Contact> contacts = await GenerateContacts(demoAdmin);
            List<Category> categories = await GenerateCategories(demoAdmin);

            //Assign category to each contact randomly
            Random random = new();
            foreach (Contact contact in contacts)
            {
                int numCat = random.Next(1, 5);
                var category = categories.OrderBy(categories => Guid.NewGuid()).Take(numCat).ToList();

                //Fix this.
                contact.Categories.AddRange(category);
            }

            foreach (Contact contact in contacts) { _dbContext.Add(contact); }
            foreach (Category category in categories) { _dbContext.Add(category); }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Category>> GenerateCategories(AppUser user)
        {
            if (user is not null)
            {
                _categories = new List<Category>()
                {
                    new Category(){AppUserId = user.Id, Name = "Coworkers"},
                    new Category(){AppUserId = user.Id, Name = "Family"},
                    new Category(){AppUserId = user.Id, Name = "Friends"},
					new Category(){AppUserId = user.Id, Name = "Business"},
					new Category(){AppUserId = user.Id, Name = "Church"}
				};
            }

            return _categories;
        }

        private async Task<List<Contact>> GenerateContacts(AppUser user)
        {
            string currentDirectory = Directory.GetCurrentDirectory().ToString();

            if (user is not null)
            {
                _contacts = new List<Contact>() {
                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Joan",
                        LastName = "Smith",
                        Address1 = "1234 Apple Street",
                        BirthDate = new DateTime(1994, 08, 16),
                        City = "Paris",
                        State = Enums.States.TX,
                        Email = "jsmith@mailinator.com",
                        ZipCode = 21000,
                        PhoneNumber = "8005552342", 
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//6.png")), 
                        ImageType = ".png" },

                    
                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Ty",
                        LastName = "Long",
                        Address1 = "11 Dell Place",
                        BirthDate = new DateTime(1979, 05, 12),
                        City = "Roanoke",
                        State = Enums.States.VA,
                        Email = "tlong@mailinator.com",
                        ZipCode = 18701,
                        PhoneNumber = "8005557984",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//1.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Gina",
                        LastName = "Torres",
                        Address1 = "15 W. Foxrun Dr.",
                        BirthDate = new DateTime(1992, 07, 22),
                        City = "Opa Locka",
                        State = Enums.States.FL,
                        Email = "gtorres@mailinator.com",
                        ZipCode = 33054,
                        PhoneNumber = "8005559592", 
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//10.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Moira",
                        LastName = "Quirk",
                        Address1 = "7144 Sunbeam St.",
                        BirthDate = new DateTime(1993, 08, 26),
                        City = "Savage",
                        State = Enums.States.MN,
                        Email = "mquirk@mailinator.com",
                        ZipCode = 55378,
                        PhoneNumber = "8005552903",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//7.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Paige",
                        LastName = "Leong",
                        Address1 = "184 Hartford Drive",
                        BirthDate = new DateTime(1968, 02, 13),
                        City = "Noblesville",
                        State = Enums.States.IN,
                        Email = "pleong@mailinator.com",
                        ZipCode = 46060,
                        PhoneNumber = "8005550602",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//9.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Lance",
                        LastName = "Reddick",
                        Address1 = "39 Atlantic Dr",
                        BirthDate = new DateTime(1987, 02, 09),
                        City = "Stamford",
                        State = Enums.States.CT,
                        Email = "lreddick@mailinator.com",
                        ZipCode = 06902,
                        PhoneNumber = "8005550378",
                    //Replace
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//2.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Nathan",
                        LastName = "Fillion",
                        Address1 = "310 Plumb Branch Ave.",
                        BirthDate = new DateTime(1967, 02, 19),
                        City = "Frankfort",
                        State = Enums.States.KY,
                    //Replace
                        Email = "nfillion@mailinator.com",
                        ZipCode = 40601,
                        PhoneNumber = "8005553245",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//3.png")),
                        ImageType = ".png" }, 

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Nolan",
                        LastName = "North",
                        Address1 = "77 Brandywine St.",
                        BirthDate = new DateTime(1982, 08, 10),
                        City = "Palm Coast",
                        State = Enums.States.FL,
                        Email = "nnorth@mailinator.com",
                        ZipCode = 32137,
                        PhoneNumber = "8005552953",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//5.png")),
                        ImageType = ".png" }
                };
            }

            return _contacts;
        }
    }
}
