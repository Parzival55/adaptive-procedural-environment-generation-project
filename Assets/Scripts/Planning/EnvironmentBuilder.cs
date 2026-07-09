using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder
{
    public List<Room> Build(
        EnvironmentPlan plan,
        int mapWidth,
        int mapHeight)
    {
        List<Room> rooms = new List<Room>();

        const int spacing = 3;

        int currentX = spacing;
        int currentZ = spacing;

        int tallestRoomInRow = 0;

        foreach (PlannedSpace space in plan.Spaces)
        {
            RoomShape shape = RoomShape.Rectangle;

            int width = 6;
            int height = 6;

            switch (space.Type)
            {
                case SpaceType.Spawn:
                    width = 8;
                    height = 8;
                    break;

                case SpaceType.Standard:
                    width = Random.Range(5, 8);
                    height = Random.Range(5, 8);
                    break;

                case SpaceType.Hub:
                    width = 10;
                    height = 10;
                    shape = RoomShape.LargeHall;
                    break;

                case SpaceType.Reward:
                    width = 8;
                    height = 8;
                    shape = RoomShape.LShape;
                    break;

                case SpaceType.Objective:
                    width = 9;
                    height = 9;
                    break;

                case SpaceType.Exit:
                    width = 8;
                    height = 8;
                    break;
            }

            // Move to the next row if the room won't fit.
            if (currentX + width >= mapWidth - spacing)
            {
                currentX = spacing;
                currentZ += tallestRoomInRow + spacing;

                tallestRoomInRow = 0;
            }

            // Stop generating if we've run out of map.
            if (currentZ + height >= mapHeight - spacing)
            {
                Debug.LogWarning(
                    "EnvironmentBuilder: Map is full. Remaining planned spaces were skipped.");
                break;
            }

            Room room = new Room(
                currentX,
                currentZ,
                width,
                height,
                shape);

            rooms.Add(room);

            currentX += width + spacing;

            if (height > tallestRoomInRow)
                tallestRoomInRow = height;
        }

        return rooms;
    }
}