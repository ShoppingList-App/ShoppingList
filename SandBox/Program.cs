using ShoppingListApp.Models;
using SQLite;
using System;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLiteAsyncConnection db = new SQLiteAsyncConnection(@"c:\Users\damage\Desktop\temp\fooba\foo.db");
            db.CreateTableAsync<StoreItem>().Wait();
            db.CreateTableAsync<ShoppingItem>().Wait();
            db.CreateTableAsync<ShoppingList>().Wait();

        }
    }
}
