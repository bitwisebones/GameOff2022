
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
                    entities.Add(new Entity
                    {
                        Name = e.Name,
                        EntityType = e.EntityType,
                        Model = model,
                        Texture = texA,
                        Position = Grid.ToWorld(e.GridPos) + e.LocalPos,
                        Dimensions = Vector3.One,
                    });
                    break;
                case EntityType.Billboard:
                    var texB = ResourceManager.Instance.Textures[e.Texture!];
                    entities.Add(new Entity
                    {
                        Name = e.Name,
                        Texture = texB,
                        Position = Grid.ToWorld(e.GridPos) + e.LocalPos,
                        Dimensions = new Vector3(texB.width / 64, texB.height / 64, 0),
                        EntityType = e.EntityType,
                    });
                    break;
                case EntityType.Quad:
                    var texC = ResourceManager.Instance.Textures[e.Texture!];
                    var xDim = _xDimDirs.Contains(e.Side) ? texC.width / 64 : 0.1f;
                    var zDim = _zDimDirs.Contains(e.Side) ? texC.width / 64 : 0.1f;
                    var modelB = LoadModelFromMesh(GenMeshCube(-xDim, 2, -zDim));
                    SetMaterialTexture(ref modelB, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texC);
                    var offset = e.Side switch
                    {
                        Direction.North => new Vector3(0, 0, -1),
                        Direction.South => new Vector3(0, 0, 1),
                        Direction.East => new Vector3(1, 0, 0),
                        Direction.West => new Vector3(-1, 0, 0),
                        _ => new Vector3(0, 0, 0),
                    };
                    entities.Add(new Entity
                    {
                        Name = e.Name,
                        EntityType = EntityType.Quad,
                        Model = modelB,
                        Texture = texC,
                        Position = Grid.ToWorld(e.GridPos) + e.LocalPos + offset
                    });
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