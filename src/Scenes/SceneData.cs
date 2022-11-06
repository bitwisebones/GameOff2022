
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
    public List<InteractableData>? Interactables { get; set; }
}

[Serializable]
public class InteractableData
{
    public string? Name { get; set; }
    public string? Texture { get; set; }
    public Vector3 GridPos { get; set; }
    public Direction Side { get; set; }
    public Vector3 LocalPos { get; set; }
    public bool IsBillboard { get; set; }
}

public class RenderBundle
{
    public RenderTexture2D RenderTexture { get; set; }
}