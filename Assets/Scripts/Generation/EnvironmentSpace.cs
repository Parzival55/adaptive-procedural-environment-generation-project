using UnityEngine;
using System.Collections.Generic;

public class EnvironmentSpace
{
    public Room Room;

    public List<EnvironmentSpace> Connections =
        new List<EnvironmentSpace>();

    public RoomFeature Feature =
        RoomFeature.None;

    public EnvironmentSpace(Room room)
    {
        Room = room;
    }
}