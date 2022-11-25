
public class DialogueManager
{
    private static DialogueManager? _instance;
    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DialogueManager();
            }
            return _instance;
        }
    }

    private Dictionary<PersonKind, List<int>> VisitedNodes = new Dictionary<PersonKind, List<int>>();
    public Dictionary<PersonKind, int> NextNode = new Dictionary<PersonKind, int>();
    private Dictionary<PersonKind, IDictionary<int, DialogueNode>> DialogueCache = new Dictionary<PersonKind, IDictionary<int, DialogueNode>>();

    private DialogueManager()
    {
        for (var i = PersonKind.Nobody; i <= PersonKind.Blacksmith; i++)
        {
            VisitedNodes[i] = new List<int>();
            NextNode[i] = 0;
        }

        DialogueCache[PersonKind.InnKeeper] = Dialogue.InnKeeperDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.InnKeepersHusband] = Dialogue.InnKeepersHusbandsDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Priest] = Dialogue.PriestDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Farmer] = Dialogue.FarmerDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Gravedigger] = Dialogue.GravediggerDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Tailor] = Dialogue.TailorDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Blacksmith] = Dialogue.BlacksmithDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Butler] = Dialogue.ButlerDialogue.ToDictionary(i => i.Id);
        DialogueCache[PersonKind.Lord] = Dialogue.LordDialogue.ToDictionary(i => i.Id);
    }

    public DialogueNode StartDialogue(PersonKind person)
    {
        var nextId = NextNode[person];
        return ContinueDialogue(person, nextId);
    }

    public DialogueNode ContinueDialogue(PersonKind person, int id)
    {
        VisitedNodes[person].Add(id);
        var cache = DialogueCache[person];
        return cache[id];
    }
}