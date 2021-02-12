using ShoppingListApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    public interface IShoppingListDataStore
    {
        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task<ShoppingList> GetShoppingListAsync(string shoppingListId);
        Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(string shoppingListId);
        Task<StoreItem> GetStoreItemAsync(string itemId);
        Task<IEnumerable<StoreItem>> GetStoreItemsAsync();

        Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text);

        Task<string> AddShoppingListAsync(ShoppingList shoppingList);
        Task<string> AddShoppingItemAsync(string shoppingListId, ShoppingItem shoppingItem);
        Task<string> AddStoreItemAsync(StoreItem storeItem);

        Task<ShoppingList> RemoveShoppingListAsync(string shoppingListId);
        Task<ShoppingItem> RemoveShoppingListItemAsync(string shoppingListId, string shoppingItemId);
    }
}
