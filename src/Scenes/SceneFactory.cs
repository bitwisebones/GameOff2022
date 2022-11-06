
using System.Numerics;
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

        var interactables = new List<Interactable>();
        foreach (var iData in data.Interactables)
        {
            var tex = ResourceManager.Instance.Textures[iData.Texture];
            var interactable = new Interactable
            {
                Name = iData.Name,
                Texture = tex,
                Position = Grid.ToWorld(iData.GridPos),
                IsBillboard = iData.IsBillboard,
                Width = tex.width,
                Height = tex.height,
            };
            interactables.Add(interactable);
        }

        return new RoamingScene
        {
            LevelModel = model,
            NavigationGrid = navGrid,
            Interactables = interactables,
        };
    }
}