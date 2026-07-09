using UnityEngine;

[System.Serializable]
public class Room
{
    public int X;
    public int Z;

    public int Width;
    public int Height;

    public Room(int x, int z, int width, int height)
    {
        X = x;
        Z = z;
        Width = width;
        Height = height;
    }

    public bool Intersects(Room other)
    {
        return !(X + Width <= other.X ||
                 other.X + other.Width <= X ||
                 Z + Height <= other.Z ||
                 other.Z + other.Height <= Z);
    }
}