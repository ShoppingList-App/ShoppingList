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

        Task<bool> AddShoppingListAsync(ShoppingList shoppingList);
        Task<bool> AddShoppingItemAsync(string shoppingListId, ShoppingItem shoppingItem);

        Task<ShoppingItem> RemoveShoppingListItemAsync(string shoppingListId, string shoppingItemId);
    }
}
