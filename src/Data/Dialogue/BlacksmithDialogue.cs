
public partial class Dialogue
{
    public static List<DialogueNode> BlacksmithDialogue = new List<DialogueNode>
    {
        D(0, "hello",
                L(1, "hello there"),
                Goodbye()
        ),
    };
}