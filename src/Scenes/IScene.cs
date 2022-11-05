public interface IScene
{
    RootGameState Update(float deltaTime, RootGameState gameState);
    void Render(float deltaTime, ref RenderBundle renderBundle);
}