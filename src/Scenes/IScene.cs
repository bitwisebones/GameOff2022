using System.Numerics;

public interface IScene
{
    void Update(float deltaTime);
    void Render(float deltaTime);
    List<Entity> Entities { get; set; }
    Vector4 FogColor { get; set; }
    float FogDensity { get; set; }
    void Init();
}