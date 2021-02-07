using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    class MockShoppingListDataStore : IShoppingListDataStore
    {
        private readonly List<ShoppingList> shoppingLists;

        public MockShoppingListDataStore()
        {
            shoppingLists = new List<ShoppingList>()
            {
                new ShoppingList { Id = Guid.NewGuid().ToString(), Text = "First List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "First Item 1", Amount = "89zhasdfg" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "First Item 2", Amount = "4565" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "First Item 3", Amount = "fvbdf" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "First Item 4", Amount = "4545rt" },
                } },
                new ShoppingList { Id = Guid.NewGuid().ToString(), Text = "Second List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 1", Amount = "89zhasdfg" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 2", Amount = "4565" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 3", Amount = "fvbdf" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 4", Amount = "4545rt" },
                } }
            };
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            return await Task.FromResult(shoppingLists);
        }

        public async Task<ShoppingList> GetShoppingListAsync(string shoppingListId)
        {
            return await Task.FromResult(GetShoppingListById(shoppingListId));
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(string shoppingListId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            return await Task.FromResult(shoppingList.Items);
        }

        public async Task<bool> AddShoppingListAsync(ShoppingList shoppingList)
        {
            shoppingLists.Add(shoppingList);
            return await Task.FromResult(true);
        }

        public async Task<bool> AddShoppingItemAsync(string shoppingListId, ShoppingItem shoppingItem)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            shoppingList.Items.Add(shoppingItem);
            return await Task.FromResult(true);
        }

        public async Task<ShoppingItem> RemoveShoppingListItemAsync(string shoppingListId, string shoppingItemId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            ShoppingItem shoppingItem = shoppingList.Items.FirstOrDefault(s => s.Id == shoppingItemId);
            shoppingList.Items.Remove(shoppingItem);

            return await Task.FromResult(shoppingItem);
        }


        private ShoppingList GetShoppingListById(string shoppingListId)
        {
            return shoppingLists.FirstOrDefault(s => s.Id == shoppingListId);
        }
    }
}
