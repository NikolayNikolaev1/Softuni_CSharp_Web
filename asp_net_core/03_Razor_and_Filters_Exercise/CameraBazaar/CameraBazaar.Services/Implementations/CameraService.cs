namespace CameraBazaar.Services.Implementations
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Models.Camera;
    using System.Collections.Generic;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext dbContext;

        public CameraService(CameraBazaarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(
            MakeType make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinIsoType minIso,
            int maxIso,
            bool isFullFrame,
            string videoResolution,
            int lightMetering,
            string description,
            string imageUrl,
            string userId)
        {
            this.dbContext
                .Cameras
                .Add(new Camera
                {
                    Make = make,
                    Model = model,
                    Price = price,
                    Quantity = quantity,
                    MinShutterSpeed = minShutterSpeed,
                    MaxShutterSpeed = maxShutterSpeed,
                    MinIso = minIso,
                    MaxIso = maxIso,
                    IsFullFrame = isFullFrame,
                    VideoResolution = videoResolution,
                    LightMetering = (LightMeteringType)lightMetering,
                    Description = description,
                    ImageUrl = imageUrl,
                    UserId = userId
                });

            this.dbContext.SaveChanges();
        }

        public ICollection<CameraListingServiceModel> All(string userId = null)
        {
            var cameras = this.dbContext
                .Cameras
                .AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                cameras = cameras.Where(c => c.UserId.Equals(userId));
            }

            return cameras
                .Select(c => new CameraListingServiceModel
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    ImageUrl = c.ImageUrl,
                    IsInStock = c.Quantity > 0
                }).ToList();
        }

        public CameraDetailsServiceModel Find(int id)
        {
            var camera = this.dbContext
                .Cameras
                .Where(c => c.Id == id);

            if (camera == null)
            {
                return null;
            }

            return camera
                .Select(c => new CameraDetailsServiceModel
                {
                    Make = c.Make,
                    Model = c.Model,
                    Price = c.Price,
                    IsInStock = c.Quantity > 0,
                    UserId = c.UserId,
                    UserName = c.User.UserName,
                    ImageUrl = c.ImageUrl,
                    IsFullFrame = c.IsFullFrame,
                    MinShutterSpeed = c.MinShutterSpeed,
                    MaxShutterSpeed = c.MaxShutterSpeed,
                    MinIso = c.MinIso,
                    MaxIso = c.MaxIso,
                    VideoResolution = c.VideoResolution,
                    Description = c.Description
                }).FirstOrDefault();
        }
    }
}
