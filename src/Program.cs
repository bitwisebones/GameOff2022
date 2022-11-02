using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace GameOff2022
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(1920, 1080, "Mouse or Man?");

            SetTargetFPS(60);

            float deltaTime = 0;
            float lastFrameTime = 0;
            while (!Raylib.WindowShouldClose())
            {
                SceneManager.Instance.CurrentScene.Update(deltaTime);

                deltaTime = GetFrameTime() - lastFrameTime;
                lastFrameTime = GetFrameTime();
            }

            Raylib.CloseWindow();
        }
    }
}