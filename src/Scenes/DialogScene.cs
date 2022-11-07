using Raylib_cs;
using static Raylib_cs.Raylib;

public class DialogScene : IScene
{
    public SceneData SceneData { get; set; }

    public RootGameState Update(float deltaTime, RootGameState gameState)
    {
        if (IsKeyPressed(KeyboardKey.KEY_ONE))
        {
            SceneManager.Instance.Pop();
        }
        return gameState;
    }

    public void Render(float deltaTime, ref RenderBundle renderBundle, RootGameState gameState)
    {
    }
}