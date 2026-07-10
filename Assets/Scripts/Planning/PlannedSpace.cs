using System.Collections.Generic;
using UnityEngine;

public class PlannedSpace
{
    public SpaceType Type;

    public Vector2Int GridPosition;

    public bool PositionAssigned = false;

    public List<PlannedSpace> Connections =
        new List<PlannedSpace>();

    public PlannedSpace(SpaceType type)
    {
        Type = type;
    }

    public void Connect(PlannedSpace other)
    {
        if (!Connections.Contains(other))
            Connections.Add(other);

        if (!other.Connections.Contains(this))
            other.Connections.Add(this);
    }
}