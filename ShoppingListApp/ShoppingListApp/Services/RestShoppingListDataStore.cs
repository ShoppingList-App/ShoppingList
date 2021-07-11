using IO.Swagger.Api;
using IO.Swagger.Client;
using ShoppingListApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingListApp.Services
{
    class RestShoppingListDataStore : IShoppingListDataStore
    {
        private ShoppingListApi shoppingListApi;
        private ShoppingItemApi shoppingItemApi;
        private StoreItemApi storeItemApi;

        public RestShoppingListDataStore()
        {
            CreateAPIs();
        }

        private void CreateAPIs()
        {
            shoppingListApi = new ShoppingListApi();
            shoppingItemApi = new ShoppingItemApi();
            storeItemApi = new StoreItemApi();
        }

        public async Task AddShoppingItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            await Task.Run(() =>
            {
                // XXX: WTF HAVE I DONE?!
                if (shoppingItem.StoreItemId == 0 && shoppingItem.StoreItem != null)
                {
                    shoppingItem.StoreItemId = shoppingItem.StoreItem.Id;
                }
                shoppingItem.Id = shoppingItemApi.AddShoppingItem(shoppingItem, shoppingListId).Id;
            });
        }

        public async Task AddShoppingListAsync(ShoppingList shoppingList)
        {
            await Task.Run(() =>
            {
                shoppingList.Id = shoppingListApi.AddShoppingList(shoppingList).Id;
            });
        }

        public async Task AddStoreItemAsync(StoreItem storeItem)
        {
            await Task.Run(() =>
            {
                storeItem.Id = storeItemApi.AddStoreItem(storeItem).Id;
            });
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(int shoppingListId)
        {
            List<ShoppingItem> shoppingItems = await Task.Run(() =>
            {
                return shoppingItemApi.GetShoppingItems(shoppingListId, false);
            });

            return shoppingItems;
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsOrderBySortKeyAsync(int shoppingListId)
        {
            List<ShoppingItem> shoppingItems = await Task.Run(() =>
            {
                return shoppingItemApi.GetShoppingItems(shoppingListId, true);
            });

            return shoppingItems;
        }

        public async Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            ShoppingList shoppingList = await Task.Run(() =>
            {
                return shoppingListApi.GetShoppingList(shoppingListId);
            });

            return shoppingList;
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            List<ShoppingList> shoppingLists = await Task.Run(() =>
            {
                return shoppingListApi.GetShoppingLists();
            });

            return shoppingLists;
        }

        public async Task<IEnumerable<StoreItem>> GetStoreItemsAsync()
        {
            List<StoreItem> storeItems = await Task.Run(() =>
            {
                return storeItemApi.GetStoreItems(null, null);
            }).ConfigureAwait(false);

            return storeItems;
        }

        public void LoginUpdate()
        {
            CreateAPIs();
        }

        public async Task RecalculateStoreItemSortAsync()
        {
            await Task.Run(() =>
            {
                storeItemApi.RecalculateStoreItemSort();
            });
        }

        public async Task RemoveShoppingListAsync(ShoppingList shoppingList)
        {
            await Task.Run(() =>
            {
                shoppingListApi.RemoveShoppingList(shoppingList.Id);
            });
        }

        public async Task RemoveShoppingListItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            await Task.Run(() =>
            {
                shoppingItemApi.RemoveShoppingItem(shoppingItem.Id);
            });
        }

        public Task ResetDatabaseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text, int limit)
        {
            List<StoreItem> storeItems = await Task.Run(() =>
            {
                return storeItemApi.GetStoreItems(text, limit);
            });

            return storeItems;
        }

        public async Task UpdateStoreItemAsync(StoreItem storeItem)
        {
            await Task.Run(() =>
            {
                storeItemApi.UpdateStoreItem(storeItem);
            });
        }
    }
}
