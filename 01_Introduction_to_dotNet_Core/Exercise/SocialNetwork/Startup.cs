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
                //dbContext.Database.EnsureDeleted();
                dbContext.Database.Migrate();

                SeedUsersWithFriendships(dbContext);
                SeedAlbumsWithPictures(dbContext);
            }
        }

        private static void SeedUsersWithFriendships(SocialNetworkDbContext db)
        {
            if (!db.Users.Any())
            {
                const int totalUsers = 50;
                Random random = new Random();

                var userList = new List<User>();

                for (int i = 0; i < totalUsers; i++)
                {

                    bool isDeleted;
                    if (i % 2 == 0)
                    {
                        isDeleted = true;
                    }
                    else
                    {
                        isDeleted = false;
                    }

                    var user = new User
                    {
                        Username = $"Username {i}",
                        Password = $"Passw0rd#{i}",
                        Email = $"user{i}@email.com",
                        RegisteredOn = DateTime.Now.AddDays(-i * 10),
                        LastTimeLoggedIn = DateTime.Now.AddDays(-i),
                        Age = i + 1,
                        IsDeleted = isDeleted
                    };

                    userList.Add(user);
                    db.Users.Add(user);
                }

                db.SaveChanges();

                var userIds = userList.Select(u => u.Id).ToList();

                for (int i = 0; i < userIds.Count; i++)
                {
                    var currentUserId = userIds[i];

                    var friendshipsCount = random.Next(1, 11);

                    var friendsIdsList = new List<int>();

                    for (int j = 0; j < friendshipsCount; j++)
                    {
                        var friendID = userIds[random.Next(0, totalUsers)];
                        var validFriendship = true;
                        if (friendID == currentUserId)
                        {
                            validFriendship = false;
                        }

                        var friendshipExist = db
                            .Friendships
                            .Any(f =>
                            (f.FromUserId == currentUserId && f.ToUserId == currentUserId)
                            || (f.FromUserId == friendID && f.ToUserId == friendID));


                        if (friendshipExist)
                        {
                            validFriendship = false;
                        }

                        if (friendsIdsList.Contains(friendID))
                        {
                            validFriendship = false;
                        }

                        if (!validFriendship)
                        {
                            j--;
                            continue;
                        }

                        db.Friendships.Add(new Friendship
                        {
                            FromUserId = currentUserId,
                            ToUserId = friendID
                        });

                        friendsIdsList.Add(friendID);

                        db.SaveChanges();
                    }
                }
            }
            else
            {
                System.Console.WriteLine("There are already users in the database.");
            }
        }

        private static void SeedAlbumsWithPictures(SocialNetworkDbContext db)
        {
            const int totalAlbums = 100;
            const int totalPictures = 500;
            Random random = new Random();
            var albumList = new List<Album>();

            var userIds = db
                .Users
                .Select(u => u.Id)
                .ToList();

            for (int i = 0; i < totalAlbums; i++)
            {
                var album = new Album
                {
                    Name = $"Album {i}",
                    BackgroundColor = $"Color {i}",
                    IsPublic = random.Next(0, 2) == 0 ? true : false,
                    OwnerId = userIds[random.Next(0, userIds.Count)]
                };
                db.Albums.Add(album);
                albumList.Add(album);
            }

            db.SaveChanges();
            var pictureList = new List<Picture>();

            for (int i = 0; i < totalPictures; i++)
            {
                var picture = new Picture
                {
                    Title = $"Picture {i}",
                    Caption = $"Caption {i}",
                    Path = $"Path/{i}"
                };

                pictureList.Add(picture);
                db.Pictures.Add(picture);
            }

            db.SaveChanges();

            var albumIds = albumList
                .Select(a => a.Id)
                .ToList();

            for (int i = 0; i < pictureList.Count; i++)
            {

                var picture = pictureList[i];
                var numberofAlbums = random.Next(1, 20);

                for (int j = 0; j < numberofAlbums; j++)
                {
                    var albumId = albumIds[random.Next(0, albumIds.Count)];

                    var pictureExistsInAlbum = db
                        .Pictures
                        .Any(p => p.Id == picture.Id && p.Albums.Any(a => a.AlbumId == albumId));

                    if (pictureExistsInAlbum)
                    {
                        j--;
                        continue;
                    }

                    picture.Albums.Add(new PictureAlbum
                    {
                        AlbumId = albumId
                    });

                    db.SaveChanges();
                }
            }


        }

        private static void PrintAllUsersWithFriendsCount(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Select(u => new
                {
                    userName = u.Username,
                    userFriendsCount = u.ToFriends.Count + u.FromFriends.Count,
                    userStatus = u.IsDeleted
                })
                .OrderByDescending(u => u.userFriendsCount)
                .ThenBy(u => u.userName);

            foreach (var user in users)
            {
                Console.WriteLine($"Username - {user.userName}");
                Console.WriteLine($"Number of friends - {user.userFriendsCount}");

                if (user.userStatus)
                {
                    Console.WriteLine("Status - active");
                }
                else
                {
                    Console.WriteLine("Status - inactive");

                }
            }
        }

        private static void PrintAllUsersWithRegDate(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Where(u => !u.IsDeleted && (u.FromFriends.Count + u.ToFriends.Count) > 5)
                .OrderBy(u => u.RegisteredOn)
                .ThenByDescending(u => u.FromFriends.Count + u.ToFriends.Count)
                .Select(u => new
                {
                    Name = u.Username,
                    FriendsCount = u.FromFriends.Count + u.ToFriends.Count,
                    DaysRegistered = (DateTime.Now - u.RegisteredOn.Value).Days
                });

            foreach (var user in users)
            {
                Console.WriteLine($"User: {user.Name}");
                Console.WriteLine($"{user.FriendsCount} Friends");
                Console.WriteLine($"{user.DaysRegistered} Days");
            }
        }
    }
}
