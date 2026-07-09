using UnityEngine;
public static class ProfileManager
{
    public static GenerationSettings GetSettings(GenerationProfile profile)
    {
        switch (profile)
        {
            case GenerationProfile.Exploration:
                return new GenerationSettings
                {
                    GridWidth = 80,
                    GridHeight = 80,
                    RoomCount = 8,
                    MinimumRoomSize = 6,
                    MaximumRoomSize = 10
                };

            case GenerationProfile.Combat:
                return new GenerationSettings
                {
                    GridWidth = 50,
                    GridHeight = 50,
                    RoomCount = 20,
                    MinimumRoomSize = 4,
                    MaximumRoomSize = 6
                };

            case GenerationProfile.Survival:
                return new GenerationSettings
                {
                    GridWidth = 100,
                    GridHeight = 100,
                    RoomCount = 10,
                    MinimumRoomSize = 5,
                    MaximumRoomSize = 8
                };

            case GenerationProfile.Puzzle:
                return new GenerationSettings
                {
                    GridWidth = 40,
                    GridHeight = 40,
                    RoomCount = 6,
                    MinimumRoomSize = 8,
                    MaximumRoomSize = 12
                };

            default:
                return new GenerationSettings
                {
                    GridWidth = 50,
                    GridHeight = 50,
                    RoomCount = 10,
                    MinimumRoomSize = 4,
                    MaximumRoomSize = 8
                };
        }
    }
}