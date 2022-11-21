
public partial class Dialogue
{
    public static List<DialogueNode> TailorDialogue = new List<DialogueNode>
    {
        D(0, "Well good morning there.",
            L(1, "I heard you've got some beautiful daffodils.", () => RootGameState.Instance.IsLookingForFlowers),
            Goodbye()),
        D(1, "Aye, that I do. But how do you know that, stranger?",
            L(2, "The gravedigger would really like some for the churchyard.")),
        D(2, "Oh would he now? That's a treat.", Continue(3)),
        D(3, "You see, that old biggot can't be bothered to ask me directly...", Continue(4)),
        D(4, "...because he doesn't like my lifestyle. Old stick in the mud!", Continue(5)),
        D(5, "Tell you what. Let's make this fun.", Continue(6)),
        D(6, "You take this letter here and deliver it to my crush...", Continue(7)),
        D(7, "...he's the butler over at the Manor. Such a sophisticated man.", Continue(8)),
        D(8, "Anyway, you deliver that to him, and I'll let you cut some...", Continue(9)),
        D(9, "...daffodils from my garden. It's only fair.",
            L(10, "Sure, of course.", () => {
                DialogueManager.Instance.NextNode[PersonKind.Tailor] = 11;
                RootGameState.Instance.Inventory.Add(ItemKind.LoveLetter);
            })),
        D(10, "See you soon!", Goodbye()),
        D(11, "Have you delivered the letter?",
            L(12, "Yes I have.",
                () => !RootGameState.Instance.Inventory.Contains(ItemKind.LoveLetter),
                () => {
                    DialogueManager.Instance.NextNode[PersonKind.Tailor] = 15;
                    RootGameState.Instance.IsLookingForClippers = true;
                    // unlock the door
                    var scene = RootGameState.Instance.SceneCache[AreaKind.Tailor];
                    var door = (Door)scene.Entities.Where(e => e is Door).First(e => ((Door)e).DoorKind == DoorKind.None);
                    door.DoorKind = DoorKind.TailorGarden;
                    door.HoverText = "Enter the garden";
                }
            ),
            L(-1, "Not yet.")),
        D(12, "Excellent! The door's unlocked for you.", Continue(13)),
        D(13, "You'll need something to cut the flowers with though.", Continue(14)),
        D(14, "I can't help you there. Sorry!", Goodbye()),
        D(15, "Hope you like the flowers!", Goodbye()),
    };
}