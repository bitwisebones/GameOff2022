
using System.Numerics;
using Raylib_cs;

public class Interactable
{
    public string? Name { get; set; }
    public Texture2D Texture { get; set; }
    public Vector3 Position { get; set; }
    public bool IsBillboard { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
}