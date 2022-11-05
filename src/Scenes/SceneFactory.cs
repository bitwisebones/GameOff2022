
using Raylib_cs;
using static Raylib_cs.Raylib;

public static class SceneFactory
{
    public static IScene Build(SceneData data)
    {
        var model = ResourceManager.Instance.Models[data.Name!];
        var texture = ResourceManager.Instance.Textures[data.Name!];
        SetMaterialTexture(ref model, 0, MaterialMapIndex.MATERIAL_MAP_DIFFUSE, ref texture);

        var navGrid = new NavigationGrid();
        navGrid.Build(data.Name!);

        return new RoamingScene
        {
            LevelModel = model,
            NavigationGrid = navGrid,
        };
    }
}