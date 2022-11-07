
using System.Numerics;
using Raylib_cs;

public class Entity
{
    public string? Name { get; set; }
    public RenderType EntityType { get; set; }
    public Texture2D Texture { get; set; }
    public Model Model { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Dimensions { get; set; }
    public bool IsInteractable { get; set; }
    public Texture2D HoverTexture { get; set; }
    public Vector3 Scale { get; set; }
    public BoundingBox BoundingBox { get; set; }
    public string? HoverText { get; set; }
    public InteractionType InteractionType { get; set; }
}