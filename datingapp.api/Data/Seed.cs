using System.Collections.Generic;
using System.Linq;
using datingapp.api.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace datingapp.api.Data
{
    public class Seed
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            if(!userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach(var user in users){
                   userManager.CreateAsync(user, "password").Wait();
                }

            }
        }
         private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            

        }

    }
}