using ContactPro_MVC.Data;
using ContactPro_MVC.Models;
using ContactPro_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.PostgresTypes;

namespace ContactPro_MVC.Services
{
    public class AddressBookService : IAddressBookService
    {
        public Task AddContactToCategoryAsync(int categoryId, int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<int>> GetContactCategoryIdsAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsContactInCategory(int categoryId, int contactId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveContactFromCategoryAsync(UnknownBackendType categoryId, int contactId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> SearchForContacts(string searchString, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
