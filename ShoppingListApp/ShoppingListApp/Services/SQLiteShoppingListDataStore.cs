using ShoppingListApp.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShoppingListApp.Services
{
    internal class SQLiteShoppingListDataStore : IShoppingListDataStore
    {
        private readonly string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + Path.DirectorySeparatorChar + "shopping.db";
        private SQLiteAsyncConnection db;

        public SQLiteShoppingListDataStore()
        {
#if DEBUG
            File.Delete(databasePath);
#endif
            InitDatabase();
        }

        private void InitDatabase()
        {
            db = new SQLiteAsyncConnection(databasePath);
            db.CreateTableAsync<StoreItem>().Wait();
            db.CreateTableAsync<ShoppingItem>().Wait();
            db.CreateTableAsync<ShoppingList>().Wait();
        }

        /* ADD */
        public async Task AddShoppingItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            await db.InsertWithChildrenAsync(shoppingItem);
            ShoppingList sl = await db.GetWithChildrenAsync<ShoppingList>(shoppingListId);
            sl.Items.Add(shoppingItem);
            await db.UpdateWithChildrenAsync(sl);
        }

        public async Task AddShoppingListAsync(ShoppingList shoppingList)
        {
            _ = await db.InsertAsync(shoppingList);
        }

        public async Task AddStoreItemAsync(StoreItem storeItem)
        {
            _ = await db.InsertAsync(storeItem);
        }

        /* GET */
        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsAsync(int shoppingListId)
        {
            ShoppingList sl = await db.GetWithChildrenAsync<ShoppingList>(shoppingListId, true);
            return sl.Items;
        }

        public async Task<IEnumerable<ShoppingItem>> GetShoppingItemsOrderBySortKeyAsync(int shoppingListId)
        {
            ShoppingList shoppingList = await db.GetWithChildrenAsync<ShoppingList>(shoppingListId, true);
            return shoppingList.Items.OrderBy(si => si.StoreItem.SortKey);
        }

        public async Task<ShoppingList> GetShoppingListAsync(int shoppingListId)
        {
            return await db.GetAsync<ShoppingList>(shoppingListId);
        }

        public async Task<IEnumerable<ShoppingList>> GetShoppingListsAsync()
        {
            return await db.GetAllWithChildrenAsync<ShoppingList>();
        }

        public async Task<IEnumerable<StoreItem>> GetStoreItemsAsync()
        {
            return await db.Table<StoreItem>().ToListAsync().ConfigureAwait(false);
        }

        /* REMOVE */
        public async Task RemoveShoppingListAsync(ShoppingList shoppingList)
        {
            await db.DeleteAsync(shoppingList, true);
        }

        public async Task RemoveShoppingListItemAsync(int shoppingListId, ShoppingItem shoppingItem)
        {
            _ = await db.DeleteAsync(shoppingItem);
        }

        /* SEARCH */
        public async Task<IEnumerable<StoreItem>> SearchStoreItemsAsync(string text)
        {
            string lowerText = text.ToLower();
            return await db.Table<StoreItem>().Where(si => si.Text.ToLower().Contains(lowerText)).ToListAsync();
        }

        /* UPDATE */
        public async Task UpdateStoreItemAsync(StoreItem storeItem)
        {
            _ = await db.UpdateAsync(storeItem);
        }

        /* DANGER ZONE */
        public async Task ResetDatabaseAsync()
        {
            await db.CloseAsync();
            db = null;
            File.Delete(databasePath);
            InitDatabase();
        }
    }
}
