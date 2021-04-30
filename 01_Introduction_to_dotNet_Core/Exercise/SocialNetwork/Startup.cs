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
            var totalAlbums = 5;
            var totalPictures = 10;
            Random random = new Random();

            var users = db
                .Users
                .Select(u => u.Id)
                .ToList();

            foreach (var user in users)
            {
                var albumList = new List<Album>();
                var pictureLsit = new List<Picture>();

                var albumCount = random.Next(1, totalAlbums);

                for (int i = 0; i < albumCount; i++)
                {
                    bool isPublic = false;
                    if (i % 2 == 0)
                    {
                        isPublic = true;
                    }

                    var album = new Album
                    {
                        Name = $"Album {i}",
                        BackgroundColor = $"Color {i}",
                        IsPublic = isPublic,
                        OwnerId = user
                    };

                    albumList.Add(album);
                    db.Albums.Add(album);
                }

                var pictureCount = random.Next(1, totalPictures);

                for (int i = 0; i < pictureCount; i++)
                {
                    var picture = new Picture
                    {
                        Title = $"Picture {i}",
                        Caption = $"Caption {i}",
                        Path = $"Path/{i}"
                    };

                    pictureLsit.Add(picture);
                    db.Pictures.Add(picture);
                }

                var pictureAlbumsCount = random.Next(1, totalAlbums * totalPictures);
                //todo: fix finish
                var comboList = new List<(int, int)>();
                for (int i = 0; i < pictureAlbumsCount; i++)
                {
                    var pictureId = random.Next(1, pictureCount + 1);
                    var albumId = random.Next(1, albumCount + 1);

                    if (comboList.Contains((pictureId, albumId)))
                    {
                        continue;
                    }

                    db.Add(new PictureAlbum
                    {
                        PictureId = pictureId,
                        AlbumId = albumId
                    });

                    var combo = (pictureId, albumId);
                    comboList.Add(combo);
                }
            }

            db.SaveChanges();

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
