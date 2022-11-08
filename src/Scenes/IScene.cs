public interface IScene
{
    void Update(float deltaTime);
    void Render(float deltaTime, ref RenderBundle renderBundle);
}