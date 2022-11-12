
using System.Numerics;
using Raylib_cs;

public abstract class Entity
{
    public string? Name { get; set; }
    public Vector3 Position { get; set; }
    public Texture2D Texture { get; set; }

    public abstract string GetHoverText();
    public abstract BoundingBox GetBoundingBox();
}

public class Terrain : Entity
{
    public Model Model { get; set; }

    public override BoundingBox GetBoundingBox() => new BoundingBox(Vector3.Zero, Vector3.Zero);
    public override string GetHoverText() => "";
}

public class Person : Entity
{
    public BoundingBox BoundingBox { get; set; }
    public Texture2D HoverTexture { get; set; }
    public string? HoverText { get; set; }
    public Vector3 Scale { get; set; }
    public PersonKind PersonKind { get; set; }

    public override BoundingBox GetBoundingBox() => BoundingBox;
    public override string GetHoverText() => HoverText!;
}

public class Item : Entity
{
    public Model Model { get; set; }
    public BoundingBox BoundingBox { get; set; }
    public Texture2D HoverTexture { get; set; }
    public string? HoverText { get; set; }
    public ItemKind ItemKind { get; set; }

    public override BoundingBox GetBoundingBox() => BoundingBox;
    public override string GetHoverText() => HoverText!;
}

public class Door : Entity
{
    public Model Model { get; set; }
    public BoundingBox BoundingBox { get; set; }
    public Texture2D HoverTexture { get; set; }
    public string? HoverText { get; set; }
    public DoorKind DoorKind { get; set; }

    public override BoundingBox GetBoundingBox() => BoundingBox;
    public override string GetHoverText() => HoverText!;
}

public class Billboard : Entity
{
    public BoundingBox BoundingBox { get; set; }
    public Texture2D HoverTexture { get; set; }
    public string? HoverText { get; set; }
    public Vector3 Scale { get; set; }

    public override BoundingBox GetBoundingBox() => BoundingBox;
    public override string GetHoverText() => "";
}