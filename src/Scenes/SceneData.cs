
using System.Numerics;
using Raylib_cs;

public enum SceneType
{
    Roaming,
    Dialog,
}

[Serializable]
public class SceneData
{
    public string? Name { get; set; }
    public SceneType SceneType { get; set; }
    public Vector3 PlayerSpawnGridPos { get; set; }
    public Direction PlayerSpawnDirection { get; set; }
}

public class RenderBundle
{
    public RenderTexture2D RenderTexture { get; set; }
}