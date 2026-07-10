using System.Collections.Generic;
using UnityEngine;

public class EnvironmentBuilder
{
    private const int CellSpacing = 20;

    public List<Room> Build(
        EnvironmentPlan plan,
        int mapWidth,
        int mapHeight)
    {
        List<Room> rooms = new List<Room>();

        Queue<PlannedSpace> queue = new Queue<PlannedSpace>();
        HashSet<PlannedSpace> visited = new HashSet<PlannedSpace>();

        if (plan.Spaces.Count == 0)
            return rooms;

        PlannedSpace start = plan.Spaces[0];
        start.GridPosition = Vector2Int.zero;

        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            PlannedSpace current = queue.Dequeue();

            if (visited.Contains(current))
                continue;

            visited.Add(current);

            Room room = CreateRoom(current);

            room.X = Mathf.Clamp(
                current.GridPosition.x * CellSpacing + 5,
                2,
                mapWidth - room.Width - 2);

            room.Z = Mathf.Clamp(
                current.GridPosition.y * CellSpacing + mapHeight / 2,
                2,
                mapHeight - room.Height - 2);

            room.Feature = ConvertFeature(current.Type);

            rooms.Add(room);

            int branchIndex = 0;

            foreach (PlannedSpace neighbour in current.Connections)
            {
                if (visited.Contains(neighbour))
                    continue;

                Vector2Int offset;

                if (branchIndex == 0)
                    offset = Vector2Int.right;
                else if (branchIndex % 2 == 1)
                    offset = Vector2Int.up;
                else
                    offset = Vector2Int.down;

                neighbour.GridPosition = current.GridPosition + offset;

                queue.Enqueue(neighbour);

                branchIndex++;
            }
        }

        return rooms;
    }

    private Room CreateRoom(PlannedSpace space)
    {
        int width = 14;
        int height = 14;

        RoomShape shape = RoomShape.Rectangle;

        switch (space.Type)
        {
            case SpaceType.Spawn:
                width = 16;
                height = 16;
                break;

            case SpaceType.Standard:
                width = Random.Range(12, 18);
                height = Random.Range(12, 18);
                break;

            case SpaceType.Hub:
                width = 24;
                height = 24;
                shape = RoomShape.LargeHall;
                break;

            case SpaceType.Reward:
                width = 16;
                height = 16;
                shape = RoomShape.LShape;
                break;

            case SpaceType.Objective:
                width = 18;
                height = 18;
                break;

            case SpaceType.Exit:
                width = 16;
                height = 16;
                break;
        }

        return new Room(0, 0, width, height, shape);
    }

    private RoomFeature ConvertFeature(SpaceType type)
    {
        switch (type)
        {
            case SpaceType.Spawn:
                return RoomFeature.Spawn;

            case SpaceType.Standard:
                return RoomFeature.Standard;

            case SpaceType.Hub:
                return RoomFeature.Hub;

            case SpaceType.Reward:
                return RoomFeature.Reward;

            case SpaceType.Objective:
                return RoomFeature.Objective;

            case SpaceType.Exit:
                return RoomFeature.Exit;

            default:
                return RoomFeature.None;
        }
    }
}