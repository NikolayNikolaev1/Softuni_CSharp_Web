namespace SocialNetwork
{
    using Microsoft.EntityFrameworkCore;
    using SocialNetwork.Data;
    using SocialNetwork.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var dbContext = new SocialNetworkDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();

                SeedUsersWithFriendships(dbContext);
            }
        }

        private static void SeedUsersWithFriendships(SocialNetworkDbContext db)
        {
            if (!db.Users.Any())
            {
                const int totalUsers = 50;
                const int totalFriendships = 10;
                Random random = new Random();

                var userList = new List<User>();

                for (int i = 0; i < totalUsers; i++)
                {
                    var user = new User
                    {
                        Username = $"Username {i}",
                        Password = $"Passw0rd#{i}",
                        Email = $"user{i}@email.com",
                        RegisteredOn = DateTime.Now.AddDays(i),
                        LastTimeLoggedIn = DateTime.Now.AddDays(i + 1),
                        Age = i + 1
                    };

                    userList.Add(user);
                    db.Users.Add(user);
                }

                db.SaveChanges();

                var userIds = db.Users.Select(u => u.Id).ToList();

                for (int i = 0; i < totalUsers; i++)
                {
                    var currentUser = userList[i];

                    for (int j = 0; j < random.Next(1, totalFriendships); j++)
                    {
                        var friendID = random.Next(0, totalUsers + 1);
                        if (!currentUser.FromFriends.Any(f => f.FromUserId == friendID))
                        {
                            currentUser.FromFriends.Add(new Friendship
                            {
                                FromUserId = friendID
                            });
                        }
                    }

                    for (int j = 0; j < random.Next(1, totalFriendships); j++)
                    {
                        var friendID = random.Next(0, totalUsers + 1);
                        if (!currentUser.ToFriends.Any(f => f.ToUserId == friendID))
                        {
                            currentUser.ToFriends.Add(new Friendship
                            {
                                ToUserId = friendID
                            });
                        }
                    }
                }

                db.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("There are already users in the database.");
            }
        }
    }
}
