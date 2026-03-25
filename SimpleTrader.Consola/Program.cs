using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework;
using SimpleTrader.EntityFramework.Services;

namespace SimpleTrader.Consola
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IDataService<User> userService = new GenericDataService<User>(new SimpleTraderDbContextFactory());
            //await userService.Create(new User
            //{
            //    Username = "testuser",
            //    Email = "test@example.com",
            //    Password = "password123"
            //});

            //Console.WriteLine(userService.GetAll().Result.Count());
            //Console.WriteLine(userService.Get(1).Result.Username);
            //var nuwUser = new User();
            //nuwUser.Username = "Ric";
            //nuwUser.Password = "password321";
            //nuwUser.Email = "test@example.com";
            //Console.WriteLine(userService.Update(1, nuwUser).Result.Username);
            Console.WriteLine(userService.Delete(1).Result);
            Console.ReadLine();
        }
    }
}
