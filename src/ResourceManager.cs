
using Raylib_cs;
using static Raylib_cs.Raylib;

public class ResourceManager
{
    private static ResourceManager? _instance;
    public static ResourceManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ResourceManager();
            }
            return _instance;
        }
    }

    private string[] _soundPaths;
    private string[] _texturePaths;
    private string[] _modelPaths;
    private string[] _imagePaths;
    private string[] _fontPaths;
    private int _index = 0;
    private int _totalFiles;

    public Dictionary<string, Sound> Sounds { get; } = new Dictionary<string, Sound>();
    public Dictionary<string, Texture2D> Textures { get; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, Model> Models { get; } = new Dictionary<string, Model>();
    public Dictionary<string, Image> Images { get; } = new Dictionary<string, Image>();
    public Dictionary<string, Font> Fonts { get; set; } = new Dictionary<string, Font>();

    private ResourceManager()
    {
        _soundPaths = Directory.GetFiles("Resources/Audio");
        _texturePaths = Directory.GetFiles("Resources/Textures");
        _modelPaths = Directory.GetFiles("Resources/Models");
        _imagePaths = Directory.GetFiles("Resources/Images");
        _fontPaths = Directory.GetFiles("Resources/Fonts");
        _totalFiles = _soundPaths.Length + _texturePaths.Length + _modelPaths.Length + _imagePaths.Length + _fontPaths.Length;

        Console.WriteLine($"{_soundPaths.Length}, {_texturePaths.Length}, {_modelPaths.Length}, {_imagePaths.Length}, {_fontPaths.Length}");
    }

    public Progress LoadNext()
    {
        if (_index < _soundPaths.Length - 1)
        {
            var idx = _index;
            var sound = LoadSound(_soundPaths[idx]);
            var fn = FormatFileName(_soundPaths[idx]);
            if (Sounds.ContainsKey(fn))
            {
                Sounds.Remove(fn);
            }
            Sounds.Add(fn, sound);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length < _texturePaths.Length)
        {
            var idx = _index - _soundPaths.Length;
            var texture = LoadTexture(_texturePaths[idx]);
            var fn = FormatFileName(_texturePaths[idx]);
            if (Textures.ContainsKey(fn))
            {
                Textures.Remove(fn);
            }
            Textures.Add(fn, texture);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length < _modelPaths.Length)
        {
            var idx = _index - _soundPaths.Length - _texturePaths.Length;
            var model = LoadModel(_modelPaths[idx]);
            var fn = FormatFileName(_modelPaths[idx]);
            if (Models.ContainsKey(fn))
            {
                Models.Remove(fn);
            }
            Models.Add(fn, model);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length < _imagePaths.Length)
        {
            var idx = _index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length;
            var image = LoadImage(_imagePaths[idx]);
            var fn = FormatFileName(_imagePaths[idx]);
            if (Images.ContainsKey(fn))
            {
                Images.Remove(fn);
            }
            Images.Add(fn, image);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length - _imagePaths.Length < _fontPaths.Length)
        {
            var idx = _index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length - _imagePaths.Length;
            var font = LoadFont(_fontPaths[idx]);
            var fn = FormatFileName(_fontPaths[idx]);
            if (Fonts.ContainsKey(fn))
            {
                Fonts.Remove(fn);
            }
            Fonts.Add(fn, font);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
    }

    public void ReloadLoadAll()
    {
        _index = 0;
        while (true)
        {
            var progress = ResourceManager.Instance.LoadNext();
            if (progress.FilesLoaded == progress.TotalFiles)
            {
                break;
            }
        }
    }

    private string FormatFileName(string fileName)
    {
        var name = Path.GetFileName(fileName).Split(".")[0];
        Console.WriteLine($"Loading {name}");
        return name;
    }
}

public class Progress
{
    public int TotalFiles { get; set; }
    public int FilesLoaded { get; set; }
}