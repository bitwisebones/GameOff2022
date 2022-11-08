using Raylib_cs;
using static Raylib_cs.Raylib;

public class DialogueScene : IScene
{
    public void Update(float deltaTime)
    {
        if (IsKeyPressed(KeyboardKey.KEY_ONE))
        {
            SceneManager.Instance.Pop();
        }
    }

    public void Render(float deltaTime, ref RenderBundle renderBundle)
    {
    }
}