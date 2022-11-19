
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
            ClearBackground(Color.BLACK);
            var text = $"Loading: {progress.FilesLoaded}/{progress.TotalFiles}";
            DrawText(text, 12, 12, 20, Color.WHITE);
        }
        EndDrawing();

        if (progress.FilesLoaded == progress.TotalFiles)
        {
            StageManager.Instance.GoTo(Stage.MainMenu);
        }
    }

    public void Deinit() { }
}