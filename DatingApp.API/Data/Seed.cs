using System.Collections.Generic;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            this._context = context;

        }

        public void SeedUsers(){

            // _context.Users.RemoveRange(_context.Users);
            // _context.SaveChanges();

            // seed users:
            var usersData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(usersData);

            foreach (var user in users)
            {
                //create the password hash
                byte[] passwordHash, PasswordSalt;
                CreatePasswordHash("password", out passwordHash, out PasswordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = PasswordSalt;
                user.Username = user.Username.ToLower();
                _context.Users.Add(user);
            }
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
           using( var hmac = new System.Security.Cryptography.HMACSHA512())
           {
               passwordSalt = hmac.Key;
               passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
            
        }
    }
}