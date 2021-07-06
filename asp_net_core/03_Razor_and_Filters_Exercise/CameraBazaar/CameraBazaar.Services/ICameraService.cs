namespace CameraBazaar.Services
{
    using Data.Enums;
    using Models.Camera;
    using System.Collections.Generic;

    public interface ICameraService
    {
        void Add(
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
            string userId);

        ICollection<CameraListingServiceModel> All(string userId = null);

        bool Delete(int id);

        CameraDetailsServiceModel Details(int id);

        bool Edit(
            int id,
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
            string imageUrl);

        CameraFormServiceModel Find(int id);
    }
}
