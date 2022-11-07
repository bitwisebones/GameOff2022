public interface IScene
{
    SceneData SceneData { get; set; }
    RootGameState Update(float deltaTime, RootGameState gameState);
    void Render(float deltaTime, ref RenderBundle renderBundle, RootGameState gameState);
}