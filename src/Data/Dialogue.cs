
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

public partial class Dialogue
{
    public static DialogueNode D(int id, string text, params DialogueLink[] links) => new DialogueNode { Id = id, Text = text, Links = links.ToList() };
    public static DialogueLink L(int id, string text) => new DialogueLink { DestinationId = id, Text = text };
    public static DialogueLink L(int id, string text, Func<bool> isValid) => new DialogueLink { DestinationId = id, Text = text, IsValid = isValid };
    public static DialogueLink L(int id, string text, Action onChosen) => new DialogueLink { DestinationId = id, Text = text, OnChosen = onChosen };
    public static DialogueLink L(int id, string text, Func<bool> isValid, Action onChosen) => new DialogueLink { DestinationId = id, Text = text, IsValid = isValid, OnChosen = onChosen };

    public static DialogueLink Continue(int dest) => new DialogueLink { Text = "[continue]", DestinationId = dest };
    public static DialogueLink Goodbye() => new DialogueLink { Text = "Goodbye.", DestinationId = -1 };
}