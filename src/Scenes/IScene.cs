public interface IScene
{
    void Update(float deltaTime);
    void Render(float deltaTime);
    List<Entity> Entities { get; set; }
}