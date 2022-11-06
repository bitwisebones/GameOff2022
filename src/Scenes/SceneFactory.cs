
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class SceneFactory
{
    private static List<Direction> _xDimDirs = new List<Direction> { Direction.North, Direction.South };
    private static List<Direction> _zDimDirs = new List<Direction> { Direction.East, Direction.West };

    public static IScene Build(SceneData data)
    {
        var navGrid = new NavigationGrid();
        navGrid.Build(data.Name!);

        var entities = new List<Entity>();
        foreach (var e in data.Entities!)
        {
            switch (e.EntityType)
            {
                case EntityType.Model:
                    var texA = ResourceManager.Instance.Textures[e.Texture!];
                    var model = ResourceManager.Instance.Models[e.Model!];
                    SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texA);

                    var bb = GetModelBoundingBox(model);
                    var bx = new BoundingBox
                    {
                        max = bb.max + Grid.ToWorld(e.GridPos) + e.LocalPos,
                        min = bb.min + Grid.ToWorld(e.GridPos) + e.LocalPos,
                    };

                    entities.Add(new Entity
                    {
                        Name = e.Name,
                        EntityType = e.EntityType,
                        Model = model,
                        Texture = texA,
                        Position = Grid.ToWorld(e.GridPos) + e.LocalPos,
                        Dimensions = Vector3.One,
                        IsInteractable = e.IsInteractable,
                        BoundingBox = bx
                    });
                    break;
                case EntityType.Billboard:
                    var texB = ResourceManager.Instance.Textures[e.Texture!];
                    var pos = Grid.ToWorld(e.GridPos) + e.LocalPos;
                    var width = texB.width / 64;
                    var height = texB.height / 64;
                    var bb3 = new BoundingBox
                    {
                        min = pos + new Vector3(-width / 2, -height / 2, -width / 2),
                        max = pos + new Vector3(width / 2, height / 2, width / 2),
                    };
                    var entityB = new Entity
                    {
                        Name = e.Name,
                        Texture = texB,
                        Position = pos,
                        Dimensions = new Vector3(width, height, 0),
                        EntityType = e.EntityType,
                        IsInteractable = e.IsInteractable,
                        BoundingBox = bb3,
                        Scale = e.Scale,
                    };

                    if (e.IsInteractable)
                    {
                        var texQ = ResourceManager.Instance.Textures[e.Texture! + "_hover"];
                        entityB.HoverTexture = texQ;
                    }

                    entities.Add(entityB);
                    break;
                case EntityType.Quad:
                    var texC = ResourceManager.Instance.Textures[e.Texture!];
                    var xDim = _xDimDirs.Contains(e.Side) ? texC.width / 64 : 0.1f;
                    var zDim = _zDimDirs.Contains(e.Side) ? texC.width / 64 : 0.1f;
                    var modelB = LoadModelFromMesh(GenMeshCube(xDim, 2, zDim));
                    SetMaterialTexture(ref modelB, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texC);

                    var bb2 = GetModelBoundingBox(modelB);
                    var bx2 = new BoundingBox
                    {
                        max = bb2.max + Grid.ToWorld(e.GridPos) + e.LocalPos,
                        min = bb2.min + Grid.ToWorld(e.GridPos) + e.LocalPos,
                    };

                    var offset = e.Side switch
                    {
                        Direction.North => new Vector3(0, 0, -1),
                        Direction.South => new Vector3(0, 0, 1),
                        Direction.East => new Vector3(1, 0, 0),
                        Direction.West => new Vector3(-1, 0, 0),
                        _ => new Vector3(0, 0, 0),
                    };

                    var entityC = new Entity
                    {
                        Name = e.Name,
                        EntityType = EntityType.Quad,
                        Model = modelB,
                        Texture = texC,
                        Position = Grid.ToWorld(e.GridPos) + e.LocalPos + offset,
                        IsInteractable = e.IsInteractable,
                        BoundingBox = bx2
                    };

                    if (e.IsInteractable)
                    {
                        var texQ = ResourceManager.Instance.Textures[e.Texture! + "_hover"];
                        entityC.HoverTexture = texQ;
                    }

                    entities.Add(entityC);
                    break;
            }
        }

        return new RoamingScene
        {
            NavigationGrid = navGrid,
            Entities = entities,
        };
    }
}