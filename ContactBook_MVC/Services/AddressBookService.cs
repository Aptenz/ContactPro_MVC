using ContactPro_MVC.Data;
using ContactPro_MVC.Models;
using ContactPro_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactPro_MVC.Services
{

    public class AddressBookService : IAddressBookService
    {
        private readonly ApplicationDbContext _context;
        
        public AddressBookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
            try
            {
                //check to see if category is in contact already
                if (!await IsContactInCategory(categoryId, contactId))
                {
                    Contact? contact = await _context.Contacts.FindAsync(contactId);
                    Category? category = await _context.Categories.FindAsync(categoryId);

                    if(category != null && contact != null) // check to see if no bad data can be passed
                    {
                        category.Contacts.Add(contact);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<Category>> GetContactCategoriesAsync(int contactId)
        {
            try
            {
                Contact? contact = await _context.Contacts.Include(c=> c.Categories)
                                                          .FirstOrDefaultAsync(c => c.Id == contactId);
                return contact.Categories;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId)
        {
            try
            {
                // grab contact and grab categories
                var contact = await _context.Contacts.Include(c => c.Categories)  
                                                     .FirstOrDefaultAsync(c => c.Id == contactId);

                // with the categories model and select just id (whats needed in SelectList)
                List<int> categoryIds = contact.Categories.Select(c => c.Id).ToList();

                return categoryIds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId)
        {
            List<Category> categories = new List<Category>();

            try
            {
                categories = await _context.Categories.Where(c => c.AppUserId == userId)
                                                      .OrderBy(c => c.Name)
                                                      .ToListAsync();
            }
            catch
            {
                throw;
            }

            return categories;
        }

        public async Task<bool> IsContactInCategory(int categoryId, int contactId)
        {
            Contact? contact = await _context.Contacts.FindAsync(contactId);

            return await _context.Categories
                                 .Include(c => c.Contacts)
                                 .Where(c => c.Id == categoryId && c.Contacts.Contains(contact))
                                 .AnyAsync(); //returns true false statement
        }

        public async Task RemoveContactFromCategoryAsync(int categoryId, int contactId)
        {
            try
            {
                if(await IsContactInCategory(categoryId,contactId))
                {
                    Contact contact = await _context.Contacts.FindAsync(contactId);
                    Category category = await _context.Categories.FindAsync(categoryId);

                    if(category != null && contact != null)
                    {
                        category.Contacts.Remove(contact);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Contact> SearchForContacts(string searchString, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
