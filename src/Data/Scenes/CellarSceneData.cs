
using System.Numerics;

public static class CellarSceneData
{
    public static SceneData GetData()
    {
        return new SceneData
        {
            Name = "cellar",
            SceneType = SceneType.Roaming,
            FogColor = new Vector4(1, 1, 1, 1),
            FogDensity = 0f,
            Entities = new List<EntityData>
            {

            }
        };
    }
}