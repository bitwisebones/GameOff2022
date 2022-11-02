using System;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace GameOff2022 // Note: actual namespace depends on the project name.
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(800, 480, "Mouse or Man?");

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