using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

public class DialogueScene : IScene
{
    private RenderTexture2D _renderTexture = LoadRenderTexture(ScreenInfo.RenderWidth, ScreenInfo.RenderHeight);
    private DialogueNode? _currentNode = null;
    private List<DialogueLink> _validLinks = new List<DialogueLink>();
    public List<Entity> Entities { get; set; } = new List<Entity>();
    public Vector4 FogColor { get; set; } = new Vector4(1, 1, 1, 1);
    public float FogDensity { get; set; } = 0f;

    private int _rowHeight = 20;
    private int _padding = 5;

    public void Init() { }

    public void Update(float deltaTime)
    {
        // on start of diaglogue scene
        if (_currentNode == null)
        {
            _currentNode = DialogueManager.Instance.StartDialogue(RootGameState.Instance.CurrentConversationTarget);
            foreach (var link in _currentNode.Links)
            {
                if (link.IsValid())
                {
                    _validLinks.Add(link);
                }
            }
        }

        // CheckForClicks
        if (IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            // which choice did we pick?
            var height = (_validLinks.Count + 1) * _rowHeight + _padding;
            var startY = ScreenInfo.RenderHeight - height + _padding + _rowHeight;
            var mouseY = GetMouseY() / ScreenInfo.Crunch;
            for (var i = 0; i < _validLinks.Count; i++)
            {
                if (mouseY > startY - _padding / 2 && mouseY < startY + _rowHeight - _padding / 2)
                {
                    _validLinks[i].OnChosen();
                    if (_validLinks[i].DestinationId == -1)
                    {
                        SceneManager.Instance.Pop();
                        return;
                    }

                    var nextId = _validLinks[i].DestinationId;
                    _currentNode = DialogueManager.Instance.ContinueDialogue(RootGameState.Instance.CurrentConversationTarget, nextId);
                    _validLinks = new List<DialogueLink>();
                    foreach (var link in _currentNode.Links)
                    {
                        if (link.IsValid())
                        {
                            _validLinks.Add(link);
                        }
                    }
                }
                startY += _rowHeight;
            }
        }
    }

    public void Render(float deltaTime)
    {
        var font = ResourceManager.Instance.Fonts["alagard"];
        var fontSize = 15;

        BeginTextureMode(_renderTexture);
        {
            ClearBackground(new Color(0, 0, 0, 0));
            if (_currentNode != null)
            {
                var height = (_validLinks.Count + 1) * _rowHeight + _padding;
                var startY = ScreenInfo.RenderHeight - height;

                DrawRectangle(0, startY, ScreenInfo.RenderWidth, height, Color.BLACK);
                startY += _padding;

                DrawTextEx(font, _currentNode.Text, new Vector2(_padding, startY), fontSize, 1, Color.VIOLET);
                startY += _rowHeight;

                for (var i = 0; i < _validLinks.Count; i++)
                {
                    var mouseY = GetMouseY() / ScreenInfo.Crunch;
                    if (mouseY > startY - _padding / 2 && mouseY < startY + _rowHeight - _padding / 2)
                    {
                        DrawRectangle(0, startY - _padding / 2, ScreenInfo.RenderWidth, _rowHeight, Color.DARKGRAY);
                    }

                    DrawTextEx(font, "  > " + _validLinks[i].Text, new Vector2(_padding, startY), fontSize, 1, Color.WHITE);
                    startY += _rowHeight;
                }
            }
            DrawTextureEx(ResourceManager.Instance.Textures["cursor"], new Vector2(GetMouseX() / ScreenInfo.Crunch, GetMouseY() / ScreenInfo.Crunch), 0.0f, 0.5f, Color.WHITE);
        }
        EndTextureMode();

        DrawTexturePro(
            _renderTexture.texture,
            new Rectangle(0, 0, -ScreenInfo.RenderWidth, ScreenInfo.RenderHeight),
            new Rectangle(ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight, ScreenInfo.ScreenWidth, ScreenInfo.ScreenHeight),
            new Vector2(0, 0),
            180,
            Color.WHITE
        );
    }
}