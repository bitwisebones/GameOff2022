
using System.Numerics;
using static Raylib_cs.Raylib;

public static class ForestGen
{
    private static Random _rnd = new Random(872343);

    public static List<EntityData> GenerateGarden()
    {
        var entities = new List<EntityData>();

        for (var i = 0; i < 25; i++)
        {
            var wx = 1 - _rnd.NextSingle() * 2;
            var wy = 1 - _rnd.NextSingle() * 2;
            var x = 3 - (int)Math.Floor(_rnd.NextSingle() * 4);

            var entity = new BillboardData
            {
                GridPos = new Vector3(x, 0, 6),
                LocalPos = new Vector3(wx, -0.6f, wy),
                Scale = Vector3.One,
                TextureName = "bush_b",
            };

            entities.Add(entity);
        }

        return entities;
    }

    public unsafe static List<EntityData> Generate()
    {
        var navGrid = new NavigationGrid();
        navGrid.Build("woods");

        var noise = ResourceManager.Instance.Images["noiseTexture"];
        var data = LoadImageColors(noise);

        var entities = new List<EntityData>();

        var trees = new List<string>{
            "tree_b",
            "tree_c",
            "tree_d",
            "tree_e",
            "tree_f",
            "tree_g",
            "tree_h",
        };

        var bushes = new List<string>{
            "bush_a",
            "bush_b",
        };

        for (var i = 0; i < 1550; i++)
        {
            var x = 32 - (int)Math.Floor(_rnd.NextSingle() * 64);
            var y = 32 - (int)Math.Floor(_rnd.NextSingle() * 64);
            var t = (int)Math.Floor(_rnd.NextSingle() * trees.Count);

            var wx = 1 - _rnd.NextSingle() * 2;
            var wy = 1 - _rnd.NextSingle() * 2;


            if (navGrid.CanNavigateToGridPos(new Vector3(x, 0, y), PlayerMode.Mouse))
            {
                continue;
            }

            var bbE = new BillboardData
            {
                GridPos = new Vector3(x, 0, y),
                LocalPos = new Vector3(wx, 5.25f, wy),
                TextureName = trees[t],
                Scale = new Vector3(15, 15, 1),
            };

            entities.Add(bbE);
        }

        for (var i = 0; i < 14550; i++)
        {
            var x = 32 - (int)Math.Floor(_rnd.NextSingle() * 64);
            var y = 32 - (int)Math.Floor(_rnd.NextSingle() * 64);
            var t = (int)Math.Floor(_rnd.NextSingle() * bushes.Count);

            var wx = 1 - _rnd.NextSingle() * 2;
            var wy = 1 - _rnd.NextSingle() * 2;

            if (navGrid.CanNavigateToGridPos(new Vector3(x, 0, y), PlayerMode.Mouse))
            {
                continue;
            }

            var bbE = new BillboardData
            {
                GridPos = new Vector3(x, 0, y),
                LocalPos = new Vector3(wx, -0.5f, wy),
                TextureName = bushes[t],
                Scale = new Vector3(1, 1, 1),
            };

            entities.Add(bbE);
        }

        return entities;
    }
}