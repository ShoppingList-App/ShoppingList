using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    internal class MockShoppingListDataStore : IShoppingListDataStore
    {
        private readonly List<StoreItem> storeItems;
        private readonly List<ShoppingList> shoppingLists;
        private int i = 0;

        public MockShoppingListDataStore()
        {
            storeItems = new List<StoreItem>() {
                new StoreItem { Id = i++, Text = "First Item 1", Unit = "Kiste", SortKey = 4 },
                new StoreItem { Id = i++, Text = "First Item 2", Unit = "Bund", SortKey = 2 },
                new StoreItem { Id = i++, Text = "First Item 3", Unit = "Stiege", SortKey = 3 },
                new StoreItem { Id = i++, Text = "First Item 4", Unit = "VPE", SortKey = 1 },
                new StoreItem { Id = i++, Text = "Second Item 1", Unit = "Kiste"},
                new StoreItem { Id = i++, Text = "Second Item 2", Unit = "Bund"},
                new StoreItem { Id = i++, Text = "Second Item 3", Unit = "Stiege"},
                new StoreItem { Id = i++, Text = "Second Item 4", Unit = "VPE"}
            };

            shoppingLists = new List<ShoppingList>()
            {
                new ShoppingList { Id = i++, Text = "First List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = i++, StoreItem = storeItems[0], Amount = 1, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[1], Amount = 2, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[2], Amount = 3, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[3], Amount = 4, Unit = "Foo" },
                } },
                new ShoppingList { Id = i++, Text = "Second List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = i++, StoreItem = storeItems[4], Amount = 1, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[5], Amount = 2, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[6], Amount = 3, Unit = "Foo" },
                   new ShoppingItem { Id = i++, StoreItem = storeItems[7], Amount = 4, Unit = "Foo" },
                } }
            };
        }

        /* GET */
        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            return await Task.FromResult(shoppingLists);
        }

        public async Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            return await Task.FromResult(GetShoppingListById(shoppingListId));
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(int shoppingListId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            return await Task.FromResult(shoppingList.Items);
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsOrderBySortKeyAsync(int shoppingListId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            IEnumerable<ShoppingItem> foo = from shoppingItem in shoppingList.Items
                                            join storeItem in storeItems on shoppingItem.StoreItem equals storeItem
                                            orderby storeItem.SortKey
                                            select shoppingItem;
            return await Task.FromResult(foo);
        }

        public async Task<IEnumerable<StoreItem>> GetStoreItemsAsync()
        {
            return await Task.FromResult(storeItems);
        }

        /* SEARCH */
        public async Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text, int limit)
        {
            string lowerText = text.ToLower();
            return await Task.FromResult(storeItems.FindAll(s => s.Text.ToLower().Contains(lowerText)).Take(limit));
        }

        /* ADD */
        public Task AddShoppingListAsync(ShoppingList shoppingList)
        {
            shoppingList.Id = i++;
            shoppingLists.Add(shoppingList);
            return Task.CompletedTask;
        }

        public Task AddShoppingItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            shoppingItem.Id = i++;
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            shoppingList.Items.Add(shoppingItem);
            return Task.CompletedTask;
        }

        public Task AddStoreItemAsync(StoreItem storeItem)
        {
            storeItem.Id = i++;
            storeItems.Add(storeItem);
            return Task.CompletedTask;
        }

        /* REMOVE */
        public Task RemoveShoppingListAsync(ShoppingList shoppingList)
        {
            _ = shoppingLists.Remove(shoppingList);
            return Task.CompletedTask;
        }

        public Task RemoveShoppingListItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            _ = shoppingList.Items.Remove(shoppingItem);
            return Task.CompletedTask;
        }

        /* UPDATE */
        public Task UpdateStoreItemAsync(StoreItem storeItem)
        {
            // nothing do, storeItem is reference, it's already updated
            return Task.CompletedTask;
        }

        /* MAINTENANCE */
        public Task RecalculateStoreItemSort()
        {
            throw new NotImplementedException();
        }

        /* DANGER ZONE */
        public Task ResetDatabaseAsync()
        {
            shoppingLists.Clear();
            storeItems.Clear();
            return Task.CompletedTask;
        }




        private ShoppingList GetShoppingListById(int shoppingListId)
        {
            return shoppingLists.FirstOrDefault(s => s.Id == shoppingListId);
        }
    }
}
