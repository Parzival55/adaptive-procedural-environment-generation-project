using UnityEngine;

[System.Serializable]
public class Room
{
    public int X;
    public int Z;

    public int Width;
    public int Height;

    public RoomShape Shape;

    public RoomFeature Feature = RoomFeature.None;

    public Room(int x, int z, int width, int height, RoomShape shape = RoomShape.Rectangle)
    {
        X = x;
        Z = z;
        Width = width;
        Height = height;
        Shape = shape;
    }

    public bool Intersects(Room other)
    {
        return !(X + Width <= other.X ||
                 other.X + other.Width <= X ||
                 Z + Height <= other.Z ||
                 other.Z + other.Height <= Z);
    }

    public Vector2Int Center
    {
        get
        {
            return new Vector2Int(
                X + Width / 2,
                Z + Height / 2);
        }
    }
}