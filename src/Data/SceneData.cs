
using System.Numerics;
using Raylib_cs;

public enum SceneType
{
    Roaming,
    Dialogue,
    CutScene1,
}

[Serializable]
public class SceneData
{
    public string? Name { get; set; }
    public SceneType SceneType { get; set; }
    public List<EntityData> Entities { get; set; } = new List<EntityData>();
}

[Serializable]
public class EntityData
{
    public string? Name { get; set; }
    public Vector3 GridPos { get; set; }
    public Vector3 LocalPos { get; set; }
    public string? TextureName { get; set; }
}

public class TerrainData : EntityData
{
    public string? ModelName { get; set; }
}

public class PersonData : EntityData
{
    public Vector3 Scale { get; set; }
    public string? HoverText { get; set; }
    public PersonKind PersonKind { get; set; }
}

public class DoorData : EntityData
{
    public Vector3 Scale { get; set; }
    public Direction Side { get; set; }
    public string? HoverText { get; set; }
    public DoorKind DoorKind { get; set; }
}

public class ItemData : EntityData
{
    public Direction Side { get; set; }
    public Vector3 Scale { get; set; }
    public string? HoverText { get; set; }
    public ItemKind ItemKind { get; set; }
}