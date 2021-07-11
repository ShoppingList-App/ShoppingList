using ShoppingListApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    public interface IShoppingListDataStore
    {
        /* GET */
        Task<IEnumerable<ShoppingList>> GetShoppingListsAsync();
        Task<ShoppingList> GetShoppingListAsync(int shoppingListId);
        Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(int shoppingListId);
        Task<IEnumerable<ShoppingItem>> GetShoppingItemsOrderBySortKeyAsync(int shoppingListId);
        Task<IEnumerable<StoreItem>> GetStoreItemsAsync();

        /* SEARCH */
        Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text, int limit);

        /* ADD */
        Task AddShoppingListAsync(ShoppingList shoppingList);
        Task AddShoppingItemAsync(int shoppingListId, ShoppingItem shoppingItem);
        Task AddStoreItemAsync(StoreItem storeItem);

        /* REMOVE */
        Task RemoveShoppingListAsync(ShoppingList shoppingList);
        Task RemoveShoppingListItemAsync(int shoppingListId, ShoppingItem shoppingItem);

        /* UPDATE */
        Task UpdateStoreItemAsync(StoreItem storeItem);

        /* MAINTENANCE */
        Task RecalculateStoreItemSortAsync();
        void LoginUpdate();

        /* DANGER ZONE */
        Task ResetDatabaseAsync();
    }
}
