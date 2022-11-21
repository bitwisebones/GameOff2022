using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class NavigationGrid
{
    private Dictionary<(int, int), bool> _navGrid = new Dictionary<(int, int), bool>();
    private Dictionary<(int, int), bool> _navGridMouse = new Dictionary<(int, int), bool>();
    private Dictionary<(int, int), bool> _transformGrid = new Dictionary<(int, int), bool>();

    public unsafe void Build(string name)
    {
        var image = ResourceManager.Instance.Images[name];
        var colors = LoadImageColors(image);
        for (var x = 0; x < image.width; x++)
        {
            for (var y = 0; y < image.height; y++)
            {
                var i = Grid.ToIndex(new Vector3(x, 0, y));
                var c = colors[i];
                if (c.b != 0)
                {
                    _navGrid[(x, y)] = true;
                }

                if (c.r != 0)
                {
                    _navGridMouse[(x, y)] = true;
                }

                if (c.g != 0)
                {
                    _transformGrid[(x, y)] = true;
                }
            }
        }
    }

    public bool CanNavigateToGridPos(Vector3 gridPos, PlayerMode mode) => mode switch
    {
        PlayerMode.Man => _navGrid.ContainsKey(((int)gridPos.X, (int)gridPos.Z)),
        _ => _navGrid.ContainsKey(((int)gridPos.X, (int)gridPos.Z)) || _navGridMouse.ContainsKey(((int)gridPos.X, (int)gridPos.Z)),
    };

    public bool CanTransform(Vector3 gridPos) => _transformGrid.ContainsKey(((int)gridPos.X, (int)gridPos.Z));

    public void AddTile((int, int) coords)
    {
        _navGrid.Add(coords, true);
        _transformGrid.Add(coords, true);
    }

    public void DebugDraw()
    {
        foreach (var k in _navGrid.Keys)
        {
            var gridPos = new Vector3(k.Item1, 0, k.Item2);
            var worldPos = Grid.ToWorld(gridPos);
            worldPos.Y = 0f;
            DrawCube(worldPos, 1, 0.1f, 1, Color.MAGENTA);
        }

        foreach (var k in _navGridMouse.Keys)
        {
            var gridPos = new Vector3(k.Item1, 0, k.Item2);
            var worldPos = Grid.ToWorld(gridPos);
            worldPos.Y = 0f;
            DrawCube(worldPos, 1, 0.1f, 1, Color.RED);
        }

        foreach (var k in _transformGrid.Keys)
        {
            var gridPos = new Vector3(k.Item1, 0, k.Item2);
            var worldPos = Grid.ToWorld(gridPos);
            worldPos.Y = 0f;
            DrawCube(worldPos, 1, 0.1f, 1, Color.WHITE);
        }
    }
}