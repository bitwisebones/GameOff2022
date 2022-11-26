using static Raylib_cs.Raylib;

namespace GameOff2022
{
    class Program
    {
        static void Main(string[] args)
        {
            SetConfigFlags(Raylib_cs.ConfigFlags.FLAG_WINDOW_RESIZABLE);
            InitWindow(1920 / 2, 1080 / 2, "Mouse or Man?");
            InitAudioDevice();

            SetExitKey(0);
            SetTargetFPS(60);

            float deltaTime = 0;
            while (!WindowShouldClose())
            {
                StageManager.Instance.CurrentScene.Update(deltaTime);
                deltaTime = GetFrameTime();
            }

            CloseWindow();
        }
    }
}