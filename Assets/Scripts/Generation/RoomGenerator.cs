using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    private readonly int mapWidth;
    private readonly int mapHeight;

    public RoomGenerator(int width, int height)
    {
        mapWidth = width;
        mapHeight = height;
    }

    public List<Room> GenerateRooms(
        int roomCount,
        int minSize,
        int maxSize)
    {
        List<Room> rooms = new List<Room>();

        int attempts = 0;
        int maxAttempts = roomCount * 10;

        //Double take on what rooms should look like - basic design or more complex design?

        while (rooms.Count < roomCount && attempts < maxAttempts)
        {
            attempts++;

            int width = Random.Range(minSize, maxSize + 1);
            int height = Random.Range(minSize, maxSize + 1);

            int x = Random.Range(1, mapWidth - width - 1);
            int z = Random.Range(1, mapHeight - height - 1);

            Room newRoom = new Room(x, z, width, height);

            bool overlaps = false;

            foreach (Room room in rooms)
            {
                if (newRoom.Intersects(room))
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                rooms.Add(newRoom);
            }
        }

        Debug.Log($"Generated {rooms.Count} rooms.");

        return rooms;
    }
}