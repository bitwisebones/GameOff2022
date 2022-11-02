
using Raylib_cs;

public class LoadingScene : IScene
{
    public void Init() { }

    public void Update(float deltaTime)
    {
        var progress = ResourceManager.Instance.LoadNext();

        Raylib.BeginDrawing();
        {
            Raylib.ClearBackground(Color.WHITE);
            var text = $"Progress: {progress.FilesLoaded}/{progress.TotalFiles}";
            Raylib.DrawText(text, 12, 12, 20, Color.BLACK);
        }
        Raylib.EndDrawing();

        if (progress.FilesLoaded == progress.TotalFiles)
        {
            SceneManager.Instance.GoTo(Scene.MainMenu);
        }
    }

    public void Deinit() { }
}