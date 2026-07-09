using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates non-overlapping procedural rooms within the grid.
/// </summary>
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

        while (rooms.Count < roomCount && attempts < maxAttempts)
        {
            attempts++;

            int width = Random.Range(minSize, maxSize + 1);
            int height = Random.Range(minSize, maxSize + 1);

            RoomShape shape;

            float random = Random.value;

            if (random < 0.20f)
            {
                shape = RoomShape.LShape;
            }
            else if (random < 0.35f)
            {
                shape = RoomShape.LargeHall;

                width += 3;
                height += 3;
            }
            else
            {
                shape = RoomShape.Rectangle;
            }

            int x = Random.Range(1, mapWidth - width - 1);
            int z = Random.Range(1, mapHeight - height - 1);

            Room newRoom = new Room(
                x,
                z,
                width,
                height,
                shape);

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