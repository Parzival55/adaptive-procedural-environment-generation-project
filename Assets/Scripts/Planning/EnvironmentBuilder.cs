using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder
{
    private const int CellSpacing = 12;

    public List<Room> Build(
        EnvironmentPlan plan,
        int mapWidth,
        int mapHeight)
    {
        List<Room> rooms = new List<Room>();

        Dictionary<PlannedSpace, Room> builtRooms =
            new Dictionary<PlannedSpace, Room>();

        Queue<PlannedSpace> queue =
            new Queue<PlannedSpace>();

        HashSet<PlannedSpace> visited =
            new HashSet<PlannedSpace>();

        if (plan.Spaces.Count == 0)
            return rooms;

        PlannedSpace start = plan.Spaces[0];

        queue.Enqueue(start);

        start.GridPosition = Vector2Int.zero;

        while (queue.Count > 0)
        {
            PlannedSpace current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visited.Add(current);

            Room room = CreateRoom(current);

            room.X =
                Mathf.Clamp(
                    current.GridPosition.x * CellSpacing + 5,
                    2,
                    mapWidth - room.Width - 2);

            room.Z =
                Mathf.Clamp(
                    current.GridPosition.y * CellSpacing + mapHeight / 2,
                    2,
                    mapHeight - room.Height - 2);

            rooms.Add(room);

            builtRooms.Add(current, room);

            int branchIndex = 0;

            foreach (PlannedSpace neighbour in current.Connections)
            {
                if (visited.Contains(neighbour))
                    continue;

                Vector2Int offset;

                if (branchIndex == 0)
                {
                    // Continue the main route
                    offset = Vector2Int.right;
                }
                else if (branchIndex % 2 == 1)
                {
                    // Branch upwards
                    offset = Vector2Int.up;
                }
                else
                {
                    // Branch downwards
                    offset = Vector2Int.down;
                }

                neighbour.GridPosition =
                    current.GridPosition + offset;

                queue.Enqueue(neighbour);

                branchIndex++;
            }
        }

        return rooms;
    }

    private Room CreateRoom(PlannedSpace space)
    {
        int width = 6;
        int height = 6;

        RoomShape shape = RoomShape.Rectangle;

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

        return new Room(
            0,
            0,
            width,
            height,
            shape);
    }
}