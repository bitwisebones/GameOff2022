
public partial class Dialogue
{
    public static List<DialogueNode> FarmerDialogue = new List<DialogueNode>
    {
        D(0, "What da ya want?",
            L(1, "The innkeeper said you were supposed to deliver eggs.", () => RootGameState.Instance.HasTalkedToInnKeeperAboutEggs),
            Goodbye()),
        D(1, "Yeah, so what. She's behind on payments!", Continue(2)),
        D(2, "And don't you get any funny ideas!", Continue(3)),
        D(3, "You'd have to be small as a mouse to sneak into my coop!",
            L(4, "Why I wouldn't dream of it, sir.")),
        D(4, "Bah. Get lost.", Goodbye())
    };
}