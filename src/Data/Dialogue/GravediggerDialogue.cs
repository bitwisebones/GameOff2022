
public partial class Dialogue
{
    public static List<DialogueNode> GravediggerDialogue = new List<DialogueNode>
    {
        D(0, "Good morning to ya.",
            L(1, "What can you tell me about the priest?", () => RootGameState.Instance.KnowsAboutTheSewer),
            L(6, "Do you know anything about the sewer?", () => RootGameState.Instance.KnowsAboutTheSewer),
            Goodbye()),
        D(1, "Well he's a rotten man for starters.", Continue(2)),
        D(2, "I swear he's been dipping into the flower fund!", Continue(3)),
        D(3, "You see, it's my responsibility to keep this place looking nice", Continue(4)),
        D(4, "but that's impossible without a budget for flowers!", Continue(5)),
        D(5, "I'd do just about anything for one of the tailor's daffodils!",
            L(0, "[continue]", () => { RootGameState.Instance.IsLookingForFlowers = true; })),
        D(6, "The sewer? I think it has something to do with the cheesery.", Continue(7)),
        D(7, "Connects the woods to Thomas Manor, if I'm not mistaken.", Continue(8)),
        D(8, "You'd think I'd know more, being the gravedigger and all!", Continue(9)),
        D(9, "But my job's to dig, and that's all.", Continue(0)),
    };
}