
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
                    entities.Add(BuildModelEntity(e));
                    break;
                case EntityType.Billboard:
                    entities.Add(BuildBillboardEntity(e));
                    break;
                case EntityType.Quad:
                    entities.Add(BuildQuadEntity(e));
                    break;
            }
        }

        return new RoamingScene
        {
            NavigationGrid = navGrid,
            Entities = entities,
        };
    }

    private static Entity BuildModelEntity(EntityData data)
    {
        var texture = ResourceManager.Instance.Textures[data.Texture!];
        var model = ResourceManager.Instance.Models[data.Model!];
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        var boundingBox = GetModelBoundingBox(model);
        var translatedBox = new BoundingBox
        {
            max = boundingBox.max + Grid.ToWorld(data.GridPos) + data.LocalPos,
            min = boundingBox.min + Grid.ToWorld(data.GridPos) + data.LocalPos,
        };

        return new Entity
        {
            Name = data.Name,
            EntityType = data.EntityType,
            Model = model,
            Texture = texture,
            Position = Grid.ToWorld(data.GridPos) + data.LocalPos,
            Dimensions = Vector3.One,
            IsInteractable = data.IsInteractable,
            BoundingBox = translatedBox
        };
    }

    private static Entity BuildBillboardEntity(EntityData data)
    {
        var texture = ResourceManager.Instance.Textures[data.Texture!];
        var position = Grid.ToWorld(data.GridPos) + data.LocalPos;
        var width = texture.width / 64;
        var height = texture.height / 64;

        var boundingBox = new BoundingBox
        {
            min = position + new Vector3(-width / 2, -height / 2, -width / 2),
            max = position + new Vector3(width / 2, height / 2, width / 2),
        };

        var entity = new Entity
        {
            Name = data.Name,
            Texture = texture,
            Position = position,
            Dimensions = new Vector3(width, height, 0),
            EntityType = data.EntityType,
            IsInteractable = data.IsInteractable,
            BoundingBox = boundingBox,
            Scale = data.Scale,
        };

        if (data.IsInteractable)
        {
            var hoverTexture = ResourceManager.Instance.Textures[data.Texture! + "_hover"];
            entity.HoverTexture = hoverTexture;
        }

        return entity;
    }

    private static Entity BuildQuadEntity(EntityData data)
    {
        var texture = ResourceManager.Instance.Textures[data.Texture!];
        var xDim = _xDimDirs.Contains(data.Side) ? texture.width / 64 : 0.1f;
        var yDim = texture.height / 64;
        var zDim = _zDimDirs.Contains(data.Side) ? texture.width / 64 : 0.1f;
        var model = LoadModelFromMesh(GenMeshCube(xDim, yDim, zDim));
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        var boundingBox = GetModelBoundingBox(model);
        var translatedBox = new BoundingBox
        {
            max = boundingBox.max + Grid.ToWorld(data.GridPos) + data.LocalPos,
            min = boundingBox.min + Grid.ToWorld(data.GridPos) + data.LocalPos,
        };

        var offset = data.Side switch
        {
            Direction.North => new Vector3(0, 0, -1),
            Direction.South => new Vector3(0, 0, 1),
            Direction.East => new Vector3(1, 0, 0),
            Direction.West => new Vector3(-1, 0, 0),
            _ => new Vector3(0, 0, 0),
        };

        var entity = new Entity
        {
            Name = data.Name,
            EntityType = EntityType.Quad,
            Model = model,
            Texture = texture,
            Position = Grid.ToWorld(data.GridPos) + data.LocalPos + offset,
            IsInteractable = data.IsInteractable,
            BoundingBox = translatedBox
        };

        if (data.IsInteractable)
        {
            var hoverTexture = ResourceManager.Instance.Textures[data.Texture! + "_hover"];
            entity.HoverTexture = hoverTexture;
        }

        return entity;
    }
}