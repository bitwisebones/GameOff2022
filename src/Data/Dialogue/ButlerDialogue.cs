
public partial class Dialogue
{
    public static List<DialogueNode> ButlerDialogue = new List<DialogueNode>
    {
        D(0, "Good morning, sir.",
            L(1, "I have a very special letter for you.",
                () => RootGameState.Instance.Inventory.Contains(ItemKind.LoveLetter),
                () => {
                    RootGameState.Instance.Inventory.Remove(ItemKind.LoveLetter);
                }),
            L(7, "What can you tell me about Magnus?"),
            Goodbye()),
        D(1, "What do you mean?",
            L(2, "Just read it, and you'll see what I mean.")),
        D(2, "Ok...*ahem*", Continue(3)),
        D(3, "My dearest Oliver",
            L(4, "I didn't mean read it out loud!"),
            L(6, "Mhm, keep going.")),
        D(4, "Oh, oh, of course. My appologies.", Continue(5)),
        D(5, "Well, in any case, thank you for delivering this to me.",
            L(-1, "You're welcome.", () => {
                DialogueManager.Instance.NextNode[PersonKind.Butler] = 0;
            })),

        D(6, "Oh, was I reading aloud? How embarrassing!", Continue(5)),
        D(7, "He's been missing for several weeks now I'm afraid.", Continue(8)),
        D(8, "It's not unlike him to take an unexpected trip...", Continue(9)),
        D(9, "...but he always lets us know. This time is different.", Goodbye()),
    };
}