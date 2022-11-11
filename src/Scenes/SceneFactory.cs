
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
            switch (e)
            {
                case TerrainData td:
                    entities.Add(BuildTerrainEntity(td));
                    break;
                case PersonData pd:
                    entities.Add(BuildPersonEntity(pd));
                    break;
                case DoorData dd:
                    entities.Add(BuildDoorEntity(dd));
                    break;
                case ItemData id:
                    entities.Add(BuildItemEntity(id));
                    break;
            }
        }

        return new RoamingScene
        {
            NavigationGrid = navGrid,
            Entities = entities,
        };
    }

    private static Terrain BuildTerrainEntity(TerrainData data)
    {
        var texture = ResourceManager.Instance.Textures[data.TextureName!];
        var model = ResourceManager.Instance.Models[data.ModelName!];
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        return new Terrain
        {
            Name = data.Name,
            Model = model,
            Texture = texture,
            Position = Grid.ToWorld(data.GridPos) + data.LocalPos,
        };
    }

    private static Person BuildPersonEntity(PersonData data)
    {
        var texture = ResourceManager.Instance.Textures[data.TextureName!];
        var position = Grid.ToWorld(data.GridPos) + data.LocalPos;
        var width = (texture.width / 64 * data.Scale.X) / 4.0f;
        var height = (texture.height / 64 * data.Scale.Y) / 4.0f;

        var boundingBox = new BoundingBox
        {
            min = position + new Vector3(-width, -height, -width),
            max = position + new Vector3(width, height, width),
        };

        var entity = new Person
        {
            Name = data.Name,
            Texture = texture,
            Position = position,
            BoundingBox = boundingBox,
            Scale = data.Scale,
            HoverText = data.HoverText,
            PersonKind = data.PersonKind,
        };

        var hoverTexture = ResourceManager.Instance.Textures[data.TextureName! + "_hover"];
        entity.HoverTexture = hoverTexture;

        return entity;
    }

    private static (float, float, float) CalculateDims(Texture2D texture, Direction side)
    {
        var xDim = _xDimDirs.Contains(side) ? texture.width / 64.0f : 0.005f;
        var yDim = texture.height / 64.0f;
        var zDim = _zDimDirs.Contains(side) ? texture.width / 64.0f : 0.005f;
        return (xDim, yDim, zDim);
    }

    private static Vector3 GetOffset(Direction side)
    {
        return side switch
        {
            Direction.North => new Vector3(0, 0, -1),
            Direction.South => new Vector3(0, 0, 1),
            Direction.East => new Vector3(1, 0, 0),
            Direction.West => new Vector3(-1, 0, 0),
            _ => new Vector3(0, 0, 0),
        };
    }

    private static Vector3 GetPosition(Vector3 gridPos, Vector3 localPos, Vector3 offset)
    {
        return Grid.ToWorld(gridPos) + localPos + offset;
    }

    private static BoundingBox GetBoundingBox(Model model, Vector3 position)
    {
        var boundingBox = GetModelBoundingBox(model);
        return new BoundingBox
        {
            max = boundingBox.max + position,
            min = boundingBox.min + position,
        };
    }

    private static Door BuildDoorEntity(DoorData data)
    {
        var texture = ResourceManager.Instance.Textures[data.TextureName!];
        var (xDim, yDim, zDim) = CalculateDims(texture, data.Side);
        var model = LoadModelFromMesh(GenMeshCube(xDim * data.Scale.X, yDim * data.Scale.Y, zDim * data.Scale.Z));
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        var offset = GetOffset(data.Side);
        var position = GetPosition(data.GridPos, data.LocalPos, offset);

        var boundingBox = GetBoundingBox(model, position);

        var entity = new Door
        {
            Name = data.Name,
            Model = model,
            Texture = texture,
            Position = position,
            BoundingBox = boundingBox,
            HoverText = data.HoverText,
            DoorKind = data.DoorKind,
        };

        var hoverTexture = ResourceManager.Instance.Textures[data.TextureName! + "_hover"];
        entity.HoverTexture = hoverTexture;

        return entity;
    }

    private static Item BuildItemEntity(ItemData data)
    {
        var texture = ResourceManager.Instance.Textures[data.TextureName!];
        var (xDim, yDim, zDim) = CalculateDims(texture, data.Side);
        var model = LoadModelFromMesh(GenMeshCube(xDim * data.Scale.X, yDim * data.Scale.Y, zDim * data.Scale.Z));
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        var offset = GetOffset(data.Side);
        var position = GetPosition(data.GridPos, data.LocalPos, offset);

        var boundingBox = GetBoundingBox(model, position);

        var entity = new Item
        {
            Name = data.Name,
            Model = model,
            Texture = texture,
            Position = position,
            BoundingBox = boundingBox,
            HoverText = data.HoverText,
            ItemKind = data.ItemKind,
        };

        var hoverTexture = ResourceManager.Instance.Textures[data.TextureName! + "_hover"];
        entity.HoverTexture = hoverTexture;

        return entity;
    }
}