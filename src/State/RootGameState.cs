
using System.Numerics;

[Serializable]
public class RootGameState
{
    public PlayerMode PlayerMode { get; set; }
    public Vector3 PlayerGridPos { get; set; }
    public Direction PlayerDirection { get; set; }
}