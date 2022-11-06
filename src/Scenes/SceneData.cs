
using System.Numerics;
using Raylib_cs;

public enum SceneType
{
    Roaming,
    Dialog,
    CutScene1,
}

[Serializable]
public class SceneData
{
    public string? Name { get; set; }
    public SceneType SceneType { get; set; }
    public Vector3 PlayerSpawnGridPos { get; set; }
    public Direction PlayerSpawnDirection { get; set; }
    public List<EntityData>? Entities { get; set; }
}

public enum EntityType
{
    Model,
    Billboard,
    Quad,
}

[Serializable]
public class EntityData
{
    public string? Name { get; set; }
    public EntityType EntityType { get; set; }
    public string? Texture { get; set; }
    public string? Model { get; set; }
    public Vector3 GridPos { get; set; }
    public Direction Side { get; set; }
    public Vector3 LocalPos { get; set; }
    public bool IsInteractable { get; set; }
    public Vector3 Scale { get; set; }
}

public class RenderBundle
{
    public RenderTexture2D RenderTexture { get; set; }
}