
public partial class Dialogue
{
    public static List<DialogueNode> GravediggerDialogue = new List<DialogueNode>
    {
        D(0, "Good morning to ya.",
            L(1, "What can you tell me about the priest?", () => RootGameState.Instance.KnowsAboutTheSewer),
            L(6, "Do you know anything about the sewer?", () => RootGameState.Instance.KnowsAboutTheSewer),
            L(10, "I got these flowers for you.",
                () => RootGameState.Instance.Inventory.Contains(ItemKind.Flowers),
                () => {
                    RootGameState.Instance.Inventory.Remove(ItemKind.Flowers);
                    RootGameState.Instance.IsLookingForFlowers = false;
                    DialogueManager.Instance.NextNode[PersonKind.Gravedigger] = 12;
                }),
            Goodbye()),
        D(1, "Well he's a rotten man for starters.", Continue(2)),
        D(2, "I swear he's been dipping into the flower fund!", Continue(3)),
        D(3, "You see, it's my responsibility to keep this place looking nice", Continue(4)),
        D(4, "but that's impossible without a budget for flowers!", Continue(5)),
        D(5, "I'd do just about anything for one of the tailor's daffodils!",
            L(0, "[continue]", () => { RootGameState.Instance.IsLookingForFlowers = true; })),
        D(6, "The sewer? I think it has something to do with the cheesery.", Continue(7)),
        D(7, "Connects the woods to the Manor, if I'm not mistaken.", Continue(8)),
        D(8, "You'd think I'd know more, being the gravedigger and all!", Continue(9)),
        D(9, "But my job's to dig, and that's all.", Continue(0)),
        D(10, "Well I'll be damned.", Continue(11)),
        D(11, "How did you convinced the tailor?? Actually, I don't want to know.", Continue(12)),
        D(12, "If there's anything I can do for you, let me know.",
            L(13, "Actually, do you have a shovel, by chance?", () => RootGameState.Instance.IsLookingForPick),
            Goodbye()),
        D(13, "Not with me. But I do have this pickaxe. Will that do?",
            L(-1, "That'll do just fine!", () => {
                RootGameState.Instance.IsLookingForPick = false;
                RootGameState.Instance.Inventory.Add(ItemKind.Pickaxe);
            })),
    };
}