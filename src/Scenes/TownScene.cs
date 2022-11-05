
using System.Numerics;

public class TownScene : Scene
{
    public override Vector3 PlayerSpawnPosition => new Vector3(1, 0, 1);
    public override Direction PlayerSpawnDirection => Direction.South;

    public TownScene() : base("town", new RoamingSceneUpdateStrategy()) { }
}