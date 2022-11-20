
public partial class Dialogue
{
    public static List<DialogueNode> InnKeepersHusbandsDialogue = new List<DialogueNode>
    {
        D(0, "*mumbles incoherently*",
            L(1, "I got this ale for you.", () => RootGameState.Instance.Inventory.Contains(ItemKind.Ale)),
            Goodbye()),
        D(1, "*hiccup* why thank you good sir.",
            L(2, "Your wife said you're an expert on local cheese.")),
        D(2, "Aye, that I am lad. *hiccup*", Continue(3)),
        D(3, "I'm the best damn cheesemaker there ever was.",
            L(4, "What can you tell me about the local cheese?")),
        D(4, "Well lad, the Thomas family has been making cheese here...", Continue(5)),
        D(5, "...for many generations. *hiccup*", Continue(6)),
        D(6, "That is, until Lord Thomas disappeared recently.", Continue(7)),
        D(7, "Twas not but a few months ago, just after the priest arrived.", Continue(8)),
        D(8, "There's something not right about that priest, I tell you.", Continue(9)),
        D(9, "Others will say it's just a coincidence, but I know better.", Continue(10)),
        D(10, "I've seen him sneaking around, you know. Being creepy.",
            L(11, "What was he doing?")),
        D(11, "He was sneaking around that old sewer entrance in the woods.", Continue(12)),
        D(12, "Up to know good, I'm sure",
            L(13, "The old sewer? Where's that?")),
        D(13, "It's on the other side of the woods, away from town.", Continue(14)),
        D(14, "You'll have to figure out a way past the stone wall.", Continue(15)),
        D(15, "Just look carefully for it, I'm sure you'll find it, lad.",
            L(16, "Thanks for the information", () => {
                DialogueManager.Instance.NextNode[PersonKind.InnKeepersHusband] = 17;
                RootGameState.Instance.KnowsAboutTheSewer = true;
                RootGameState.Instance.Inventory.Remove(ItemKind.Ale);
            })),
        D(16, "Be careful.", Goodbye()),
        D(17, "Any luck finding that old sewer, lad?", Goodbye()),
    };
}