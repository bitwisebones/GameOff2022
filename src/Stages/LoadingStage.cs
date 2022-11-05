
using Raylib_cs;
using static Raylib_cs.Raylib;

public class LoadingStage : IStage
{
    public void Init() { }

    public void Update(float deltaTime)
    {
        var progress = ResourceManager.Instance.LoadNext();

        BeginDrawing();
        {
            ClearBackground(Color.WHITE);
            var text = $"Progress: {progress.FilesLoaded}/{progress.TotalFiles}";
            DrawText(text, 12, 12, 20, Color.BLACK);
        }
        EndDrawing();

        if (progress.FilesLoaded == progress.TotalFiles)
        {
            StageManager.Instance.GoTo(Scene.Game);
        }
    }

    public void Deinit() { }
}