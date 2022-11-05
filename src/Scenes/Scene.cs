
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public abstract class Scene
{
    public Model LevelModel { get; set; }
    public NavigationGrid NavigationGrid { get; set; }
    public Vector3 PlayerGridPos { get; set; }
    public Direction PlayerDirection { get; set; }

    public abstract Vector3 PlayerSpawnPosition { get; }
    public abstract Direction PlayerSpawnDirection { get; }

    private ISceneUpdateStrategy _updateStrategy;

    public Scene(string name, ISceneUpdateStrategy updateStrategy)
    {
        _updateStrategy = updateStrategy;

        var model = ResourceManager.Instance.Models[name];
        var texture = ResourceManager.Instance.Textures[name];
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        LevelModel = model;
        NavigationGrid = new NavigationGrid();
        NavigationGrid.Build(name);

        PlayerGridPos = PlayerSpawnPosition;
        PlayerDirection = PlayerSpawnDirection;
    }

    public void Update(float deltaTime)
    {
        _updateStrategy.Update(deltaTime);
    }
}