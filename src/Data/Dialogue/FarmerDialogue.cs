
public partial class Dialogue
{
    public static List<DialogueNode> FarmerDialogue = new List<DialogueNode>
    {
        D(0, "What do ye want?",
            L(1, "The innkeeper said you were supposed to deliver eggs.", () => RootGameState.Instance.IsLookingForEggs),
            L(5, "Do you happen to have any clippers?", () => RootGameState.Instance.IsLookingForClippers),
            Goodbye()),
        D(1, "Yeah, so what. She's behind on payments!", Continue(2)),
        D(2, "And don't ye get any funny ideas!", Continue(3)),
        D(3, "Ye'd have to be small as a mouse to sneak into my coop!",
            L(4, "Why I wouldn't dream of it, sir.")),
        D(4, "Bah. Get lost.", Goodbye()),

        D(5, "Well of course I do. Why?",
            L(6, "I need to borrow them to cut some flowers"),
            L(6, "It's a long story.")),

        D(6, "Well I ain't gonna just give em' to ye!", Continue(7)),
        D(7, "I'll tell ye what.", Continue(8)),
        D(8, "The blacksmith owes me a horseshoe. I was about to go pick it up.", Continue(9)),
        D(9, "Why don't ye run into town and pick it up for me?", Continue(10)),
        D(10, "Then I'll gladly lend you my clippers",
            L(-1, "Ok, sure", () => {
                RootGameState.Instance.IsLookingForHorseshoes = true;
                DialogueManager.Instance.NextNode[PersonKind.Farmer] = 11;
            })),
        D(11, "Pick up that horseshoe yet?",
            L(12, "Yes, it's right here.", () => RootGameState.Instance.Inventory.Contains(ItemKind.Horseshoe)),
            L(-1, "Not yet.")),
        D(12, "Why thank ye lad. Here are those clippers ye wanted.",
            L(-1, "Thank you.", () => {
                RootGameState.Instance.Inventory.Remove(ItemKind.Horseshoe);
                RootGameState.Instance.Inventory.Add(ItemKind.Clippers);
                RootGameState.Instance.IsLookingForClippers = false;
                DialogueManager.Instance.NextNode[PersonKind.Farmer] = 0;
            })),
    };
}