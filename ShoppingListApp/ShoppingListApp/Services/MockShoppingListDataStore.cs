using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    class MockShoppingListDataStore : IShoppingListDataStore
    {
        private readonly List<StoreItem> storeItems;
        private readonly List<ShoppingList> shoppingLists;

        public MockShoppingListDataStore()
        {
            storeItems = new List<StoreItem>() {
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "First Item 1", Unit = "Kiste", SortKey = 4 },
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "First Item 2", Unit = "Bund", SortKey = 2 },
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "First Item 3", Unit = "Stiege", SortKey = 3 },
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "First Item 4", Unit = "VPE", SortKey = 1 },
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 1", Unit = "Kiste"},
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 2", Unit = "Bund"},
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 3", Unit = "Stiege"},
                new StoreItem { Id = Guid.NewGuid().ToString(), Text = "Second Item 4", Unit = "VPE"}
            };

            shoppingLists = new List<ShoppingList>()
            {
                new ShoppingList { Id = Guid.NewGuid().ToString(), Text = "First List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[0].Id , Amount = 1, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[1].Id, Amount = 2, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[2].Id, Amount = 3, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[3].Id, Amount = 4, Unit = "Foo" },
                } },
                new ShoppingList { Id = Guid.NewGuid().ToString(), Text = "Second List", Items = new List<ShoppingItem>() {
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[4].Id, Amount = 1, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[5].Id, Amount = 2, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[6].Id, Amount = 3, Unit = "Foo" },
                   new ShoppingItem { Id = Guid.NewGuid().ToString(), StoreItemId = storeItems[7].Id, Amount = 4, Unit = "Foo" },
                } }
            };
        }

        /* GET */
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

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsOrderBySortKeyAsync(string shoppingListId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            IEnumerable<ShoppingItem> foo = from shoppingItem in shoppingList.Items
                                            join storeItem in storeItems on shoppingItem.StoreItemId equals storeItem.Id
                                            orderby storeItem.SortKey
                                            select shoppingItem;
            return await Task.FromResult(foo);
        }

        public async Task<StoreItem> GetStoreItemAsync(string itemId)
        {
            return await Task.FromResult(GetStoreItemById(itemId));
        }

        public async Task<IEnumerable<StoreItem>> GetStoreItemsAsync()
        {
            return await Task.FromResult(storeItems);
        }

        /* SEARCH */
        public async Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text)
        {
            string lowerText = text.ToLower();
            return await Task.FromResult(storeItems.FindAll(s => s.Text.ToLower().Contains(lowerText)));
        }

        /* ADD */
        public async Task<string> AddShoppingListAsync(ShoppingList shoppingList)
        {
            shoppingList.Id = Guid.NewGuid().ToString();
            shoppingLists.Add(shoppingList);
            return await Task.FromResult(shoppingList.Id);
        }

        public async Task<string> AddShoppingItemAsync(string shoppingListId, ShoppingItem shoppingItem)
        {
            shoppingItem.Id = Guid.NewGuid().ToString();

            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            shoppingList.Items.Add(shoppingItem);
            return await Task.FromResult(shoppingItem.Id);
        }

        public async Task<string> AddStoreItemAsync(StoreItem storeItem)
        {
            storeItem.Id = Guid.NewGuid().ToString();

            storeItems.Add(storeItem);
            return await Task.FromResult(storeItem.Id);
        }

        /* REMOVE */
        public async Task<ShoppingList> RemoveShoppingListAsync(string shoppingListId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            _ = shoppingLists.Remove(shoppingList);

            return await Task.FromResult(shoppingList);
        }

        public async Task<ShoppingItem> RemoveShoppingListItemAsync(string shoppingListId, string shoppingItemId)
        {
            ShoppingList shoppingList = GetShoppingListById(shoppingListId);
            ShoppingItem shoppingItem = shoppingList.Items.FirstOrDefault(s => s.Id == shoppingItemId);
            _ = shoppingList.Items.Remove(shoppingItem);

            return await Task.FromResult(shoppingItem);
        }

        /* UPDATE */
        public async Task<bool> UpdateStoreItemSortKeyAsync(string storeItemId, uint sortKey)
        {
            StoreItem storeItem = GetStoreItemById(storeItemId);
            storeItem.SortKey = sortKey;
            return await Task.FromResult(true);
        }





        private ShoppingList GetShoppingListById(string shoppingListId)
        {
            return shoppingLists.FirstOrDefault(s => s.Id == shoppingListId);
        }
        private StoreItem GetStoreItemById(string storeItemId)
        {
            return storeItems.FirstOrDefault(s => s.Id == storeItemId);
        }
    }
}
