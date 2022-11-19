
public class DialogueNode
{
    public int Id { get; set; }
    public string Text { get; set; } = "";
    public List<DialogueLink> Links { get; set; } = new List<DialogueLink>();
}

public class DialogueLink
{
    public string Text { get; set; } = "";
    public int DestinationId { get; set; }
    public Func<bool> IsValid { get; set; } = () => true;
    public Action OnChosen { get; set; } = () => { };
}

public class Dialogue
{
    public static DialogueNode GetDialogue(PersonKind person)
    {
        switch (person)
        {
            case PersonKind.Blacksmith:
                return BlacksmithDialogue[1];
        }

        return BlacksmithDialogue[1];
    }

    public static DialogueLink Continue(int dest) => new DialogueLink { Text = "Continue...", DestinationId = dest };
    public static DialogueLink Goodbye() => new DialogueLink { Text = "Goodbye.", DestinationId = -1 };

    public static Dictionary<int, DialogueNode> BlacksmithDialogue = new Dictionary<int, DialogueNode>
    {
        { 1, new DialogueNode {
                Id = 1,
                Text = "What do you want?",
                Links = new List<DialogueLink>{
                    new DialogueLink{ Text = "How are you today?", DestinationId = 2},
                    new DialogueLink{ Text = "Goodbye!", DestinationId = -1},
                }
        }},
        {2, new DialogueNode {
            Id = 2,
            Text = "Harumph!",
            Links = new List<DialogueLink>{
                new DialogueLink{
                    Text = "Are you looking for a key?",
                    DestinationId = -1,
                    IsValid = () => RootGameState.Instance.Inventory.Contains(ItemKind.BlacksmithKey),
                    OnChosen = () => RootGameState.Instance.Inventory.Remove(ItemKind.BlacksmithKey),
                },
                Goodbye(),
            }
        }},
    };
}