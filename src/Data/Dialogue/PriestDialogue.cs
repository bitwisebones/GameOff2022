
public partial class Dialogue
{
    public static List<DialogueNode> PriestDialogue = new List<DialogueNode>
    {
        D(0, "Peace be with you, traveler.",
            L(1, "What brought you to Cheeseshire?"),
            L(2, "What can you tell me about the town?"),
            L(3, "What do you know about Magnus?"),
            Goodbye()),
        D(1, "I was transferred here as part of the normal rotation.", Continue(0)),
        D(2, "It's a wonderful place for people who love cheese!", Continue(0)),
        D(3, "The locals all love him, that's for sure.", Continue(4)),
        D(4, "He's overrated if you ask me.", Continue(5)),
        D(5, "But it's not polite to speak ill of someone that's gone missing.", Goodbye()),
    };
}