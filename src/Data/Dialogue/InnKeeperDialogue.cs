
public partial class Dialogue
{
    public static List<DialogueNode> InnKeeperDialogue = new List<DialogueNode>
    {
        D(0, "Morning! Sleep well last night, did you?",
            L(1, "Well enough, thank you."),
            L(2, "Could've been better..."),
            Goodbye()),
        D(1, "That's good to hear.", Continue(3)),
        D(2, "Sorry to hear that.", Continue(3)),
        D(3, "Anyway, what brings you to Cheeseshire?",
            L(4, "I'm here for the cheese of course!"),
            L(4, "To be honest, I'm not really sure.")),
        D(4, "Well if you're looking to learn a bit about cheese...", Continue(5)),
        D(5, "...my dear husband Liam knows everything about cheese!",
            L(6, "Is that so?")),
        D(6, "He was the Big Cheese at the cheesery in town...", Continue(7)),
        D(7, "...until Lord Thomas disappeared and it closed down.", Continue(8)),
        D(8, "Now he just drinks away his problems here in the tavern", Continue(9)),
        D(9, "Probably won't even talk to you unless you buy him an ale!",
            L(10, "[continue]", () => DialogueManager.Instance.NextNode[PersonKind.InnKeeper] = 10)),
        D(10, "What can I get for you dear?",
            L(11, "Can I have an ale?", () => !RootGameState.Instance.Inventory.Contains(ItemKind.Egg)),
            L(12, "Can I have some breakfast?",
                () => !RootGameState.Instance.Inventory.Contains(ItemKind.Egg),
                () => { RootGameState.Instance.IsLookingForEggs = true; }),
            L(16, "Here, I got some eggs for you.",
                () => RootGameState.Instance.Inventory.Contains(ItemKind.Egg),
                () => {
                    RootGameState.Instance.IsLookingForEggs = false;
                }),
            Goodbye()),
        D(11, "Have you got any money?",
            L(15, "No...")),
            D(15, "Sorry dear, but I'm not running a charity.", Continue(10)),
        D(12, "Sorry dear, but I don't have anything for breakfast.", Continue(13)),
        D(13, "That lazy farmer was supposed to bring me some eggs...", Continue(14)),
        D(14, "...but he hasn't shown up yet!", Continue(10)),
        D(16, "Oh thank you! Here's an ale, on the house!",
            L(17, "Great, thanks!", () => {
                RootGameState.Instance.Inventory.Remove(ItemKind.Egg);
                RootGameState.Instance.Inventory.Add(ItemKind.Ale);
                DialogueManager.Instance.NextNode[PersonKind.InnKeeper] = 17;
            })),
        D(17, "Enjoy your time in Cheeseshire!", Goodbye())
    };
}