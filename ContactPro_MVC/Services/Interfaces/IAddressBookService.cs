using ContactPro_MVC.Models;
using Npgsql.PostgresTypes;

namespace ContactPro_MVC.Services.Interfaces
{
    public interface IAddressBookService
    {
        Task AddContactToCategoryAsync(int categoryId, int contactId);
        Task<bool> IsContactInCategory(int categoryId, int contactId);
        Task<IEnumerable<Category>> GetUserCategoriesAsync(string userId);
        Task <ICollection<int>> GetContactCategoryIdsAsync(int contactId);
        Task RemoveContactFromCategoryAsync(UnknownBackendType categoryId, int contactId);
        IEnumerable<Contact> SearchForContacts(string searchString, string userId);
    }
}
