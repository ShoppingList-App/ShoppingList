
using Xamarin.Forms;

namespace ShoppingListApp.Models
{
    class Config
    {
        public static Config FromApplicationProperties(Application app)
        {
            Config conf = new Config
            {
                Host = app.Properties["host"].ToString(),
                Username = app.Properties["username"].ToString(),
                Password = app.Properties["password"].ToString()
            };

            return conf;
        }

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
