
public enum DialogueResult
{
    NextMessage,
    Quit,
}

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
    public DialogueNode FarmerDialogue = new DialogueNode
    {
        Id = 1,
        Text = "What do you want?",
        Links = new List<DialogueLink>{
            new DialogueLink{ Text = "Goodbye!", DestinationId = -1 }
        }
    };
}