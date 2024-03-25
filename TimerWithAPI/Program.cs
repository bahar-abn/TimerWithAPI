using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
    .AddSingleton<IUserService, UserService>()
    .BuildServiceProvider();








var timer = new Timer(state =>
{
    var userService = serviceProvider.GetRequiredService<IUserService>();
    var users = userService.GetUsers();

    foreach (var user in users)
    {
        Console.WriteLine($"User ID: {user.Id}, Name: {user.Name}");
    }
}, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:31093");

var response = httpClient.GetAsync("endpoint").Result;
if (response.IsSuccessStatusCode)
{
    var result = response.Content.ReadAsStringAsync().Result;
    Console.WriteLine(result);
}



public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public interface IUserService
{
    List<User> GetUsers();
}

public class UserService : IUserService
{
    public List<User> GetUsers()
    {
        
        return new List<User>
        {
            new User { Id = 1, Name = "Bahar" },
            new User { Id = 2, Name = "Amir" },
            new User { Id = 3, Name = "Raha" }
        };
    }
}
