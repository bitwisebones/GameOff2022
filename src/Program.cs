using static Raylib_cs.Raylib;

namespace GameOff2022
{
    class Program
    {
        static void Main(string[] args)
        {
            InitWindow(1920, 1080, "Mouse or Man?");
            InitAudioDevice();

            SetTargetFPS(60);

            float deltaTime = 0;
            float lastFrameTime = 0;
            while (!WindowShouldClose())
            {
                StageManager.Instance.CurrentScene.Update(deltaTime);

                deltaTime = GetFrameTime() - lastFrameTime;
                lastFrameTime = GetFrameTime();
            }

            CloseWindow();
        }
    }
}