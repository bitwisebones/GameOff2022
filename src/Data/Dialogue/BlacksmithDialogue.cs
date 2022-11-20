
public partial class Dialogue
{
    public static List<DialogueNode> BlacksmithDialogue = new List<DialogueNode>
    {
        D(0, "What a rotten morning.",
            L(1, "Sounds like someone woke up on the wrong side of the bed."),
            L(1, "Uh oh. What's wrong?"),
            Goodbye()),
        D(1, "I lost my damn keys, alright!", Continue(2)),
        D(2, "Can't get any work done if I can't unlock my toolboxes!",
            L(3, "Have you checked your pockets?")),
        D(3, "Real funny, lad. Real funny.",
            L(4, "Did you drop them? Where have you been recently?")),
        D(4, "Well, yesterday I installed some new iron fencing...", Continue(5)),
        D(5, "...in the rear gardens of the Manor. Must've dropped 'em there.",
            L(6, "I'm sure they'll turn up!", () => DialogueManager.Instance.NextNode[PersonKind.Blacksmith] = 7)),
        D(6, "Yeah, I hope so.", Goodbye()),
        D(7, "Did you happen to find my keys?",
            L(8,
                "I did! Here they are.",
                () => RootGameState.Instance.Inventory.Contains(ItemKind.BlacksmithKey),
                () => {
                    RootGameState.Instance.Inventory.Remove(ItemKind.BlacksmithKey);
                    DialogueManager.Instance.NextNode[PersonKind.Blacksmith] = 9;
                }),
            L(-1, "Not yet.")),
        D(8, "You're a life saver!", Continue(9)),
        D(9, "Anything I can do to return the favor?",
            L(10, "Well, I am looking for a horseshoe...",
                () => RootGameState.Instance.IsLookingForHorseshoes,
                () => { RootGameState.Instance.Inventory.Add(ItemKind.Horseshoe); }),
            L(-1, "I'll let you know if I think of anything.")),
        D(10, "Say no more! Here you go!",
            L(-1, "Thank you so much!",
                () => { DialogueManager.Instance.NextNode[PersonKind.Blacksmith] = 11; })),
        D(11, "Thanks again!", Goodbye()),
    };
}