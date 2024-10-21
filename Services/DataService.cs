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

        public DataService(ApplicationDbContext dbContext,
            UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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

            //Step 1: Create a new instance of BlogUser
            var demoUser = new AppUser()
            {
                Email = "demouser@mailinator.com",
                UserName = "demouser@mailinator.com",
                FirstName = "Joe",
                LastName = "DemoUser",
                PhoneNumber = "(800)555-1212",
                EmailConfirmed = true
            };

            //Step 2: Use the UserManager to create a new user that is defined by adminUser
            await _userManager.CreateAsync(demoUser, "Learntocode1!");

            await _dbContext.SaveChangesAsync();

            List<Contact> contacts = await GenerateContacts(demoUser);
            List<Category> categories = await GenerateCategories(demoUser);

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
                    new Category(){AppUserId = user.Id, Name = "Work"},
                    new Category(){AppUserId = user.Id, Name = "Family"},
                    new Category(){AppUserId = user.Id, Name = "Friends"}
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
                        PhoneNumber = "0000000000",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//6.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Erin",
                        LastName = "Doe",
                        Address1 = "1 Katz Place",
                        BirthDate = new DateTime(1976, 01, 02),
                        City = "Charleston",
                        State = Enums.States.WV,
                        Email = "edoe@mailinator.com",
                        ZipCode = 18701,
                        PhoneNumber = "0000000000",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//8.png")),
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
                        PhoneNumber = "0000000000",
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
                        PhoneNumber = "0000000000",
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
                        PhoneNumber = "0000000000",
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
                        PhoneNumber = "0000000000",
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
                        PhoneNumber = "0000000000",
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
                        Email = "nfillion@mailinator.com",
                        ZipCode = 40601,
                        PhoneNumber = "0000000000",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//3.png")),
                        ImageType = ".png" },

                    new Contact() {
                        AppUserID = user.Id,
                        FirstName = "Neil",
                        LastName = "Kaplan",
                        Address1 = "679 N. Annadale Dr.",
                        BirthDate = new DateTime(1978, 05, 02),
                        City = "Hephzibah",
                        State = Enums.States.GA,
                        Email = "nkaplan@mailinator.com",
                        ZipCode = 30815,
                        PhoneNumber = "0000000000",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//4.png")),
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
                        PhoneNumber = "0000000000",
                        ImageData = File.ReadAllBytes(Path.Combine(currentDirectory, "wwwroot//img//5.png")),
                        ImageType = ".png" }
                };
            }

            return _contacts;
        }
    }
}
