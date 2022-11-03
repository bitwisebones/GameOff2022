using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class NavigationGrid
{
    private Dictionary<int, bool> _navGrid = new Dictionary<int, bool>();
    private Dictionary<int, bool> _navGridMouse = new Dictionary<int, bool>();

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
                    _navGrid[i] = true;
                }
                else if (c.r != 0)
                {
                    _navGridMouse[i] = true;
                }
            }
        }
    }

    public bool CanNavigateToGridPos(Vector3 gridPos, PlayerMode mode)
    {
        var idx = Grid.ToIndex(gridPos);
        return mode switch
        {
            PlayerMode.Man => _navGrid.ContainsKey(idx),
            PlayerMode.Mouse => _navGrid.ContainsKey(idx) || _navGridMouse.ContainsKey(idx),
        };
    }

    public void DebugDraw()
    {
        foreach (var k in _navGrid.Keys)
        {
            var gridPos = Grid.FromIndex(k);
            var worldPos = Grid.ToWorld(gridPos);
            worldPos.Y = -0.4f;
            DrawCube(worldPos, 1, 1, 1, Color.MAGENTA);
        }

        foreach (var k in _navGridMouse.Keys)
        {
            var gridPos = Grid.FromIndex(k);
            var worldPos = Grid.ToWorld(gridPos);
            worldPos.Y = -0.4f;
            DrawCube(worldPos, 1, 1, 1, Color.RED);
        }
    }
}