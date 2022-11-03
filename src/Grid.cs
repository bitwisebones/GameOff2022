
using System.Numerics;

public enum Direction
{
    North,
    East,
    South,
    West,
}

public static class Grid
{
    public static int Size { get; } = 2;
    public static int Width { get; } = 16;
    public static int Height { get; } = 16;

    public static Vector3 ToWorld(Vector3 gridPos) => new Vector3(
            (gridPos.X * Size) + Size / 2,
            (gridPos.Y * Size) + Size / 2,
            (gridPos.Z * Size) + Size / 2
        );

    public static int ToIndex(Vector3 gridPos) => (int)((gridPos.Z * Width) + gridPos.X);

    public static Vector3 FromIndex(int index)
    {
        return new Vector3(index % Width, 0, (int)(index / (float)Width));
    }
}