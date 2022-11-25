
public partial class Dialogue
{
    public static List<DialogueNode> LordDialogue = new List<DialogueNode>
    {
        D(0, "Thank god someone finally found me!",
            L(1, "What are you doing down here?")),
        D(1, "It's complicated. I'm not sure you'd understand...", Continue(2)),
        D(2, "Wait...how did you find your way here?",
            L(3, "I followed my nose."),
            L(3, "I have an insatiable desire for cheese....")),
        D(3, "Ah, now it makes sense. You're a Weremouse like me!",
            L(4, "You're a weremouse?!?!"),
            L(4, "That explains the cheese I guess.")),
        D(4, "Hah! The Apodemus family guards many secrets...", Continue(5)),
        D(5, "...and our cheesemaking technique is the least interesting among them.",
            L(6, "How did you get down here?")),
        D(6, "That bastard priest trapped me here.", Continue(7)),
        D(7, "His kind and our kind have been at war for generations.",
            L(8, "What do you mean 'his kind'?")),
        D(8, "Dear lad, you have much to learn...", Continue(9)),
        D(9, "The new priest is a werecat!", Continue(10)),
        D(10, "I'm afraid we don't have much time to discuss...", Continue(11)),
        D(11, "He could be back at any minute! You have to free me!", Continue(12)),
        D(12, "Pick the lock to get me out!",
            L(-2, "Ok...", () => { DialogueManager.Instance.NextNode[PersonKind.Lord] = 13; })),
        D(13, "Thank you, lad!", Continue(14)),
        D(14, "Now let's get that priest!",
            L(15, "How are we going to do that?")),
        D(15, "Dont worry, there are many ways to skin a cat!",
            L(-3, "[continue]")),
    };
}