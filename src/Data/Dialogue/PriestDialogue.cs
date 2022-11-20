
public partial class Dialogue
{
    public static List<DialogueNode> PriestDialogue = new List<DialogueNode>
    {
        D(0, "Peace be with you, traveler.",
            L(1, "What brought you to Cheeseshire?"),
            L(2, "What can you tell me about the town?"),
            Goodbye()),
        D(1, "I was transferred here as part of the normal rotation.", Continue(0)),
        D(2, "It's a wonderful place for people who love cheese!", Continue(0)),
    };
}