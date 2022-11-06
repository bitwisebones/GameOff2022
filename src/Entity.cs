
using System.Numerics;
using Raylib_cs;

public class Entity
{
    public string? Name { get; set; }
    public EntityType EntityType { get; set; }
    public Texture2D Texture { get; set; }
    public Model Model { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Dimensions { get; set; }
}