
using Raylib_cs;
using System.Collections.Generic;
using System.IO;
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
    private int _index = 0;
    private int _totalFiles;

    public Dictionary<string, Sound> Sounds { get; } = new Dictionary<string, Sound>();
    public Dictionary<string, Texture2D> Textures { get; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, Model> Models { get; } = new Dictionary<string, Model>();

    private ResourceManager()
    {
        _soundPaths = Directory.GetFiles("Resources/Audio");
        _texturePaths = Directory.GetFiles("Resources/Textures");
        _modelPaths = Directory.GetFiles("Resources/Models");
        _totalFiles = _soundPaths.Length + _texturePaths.Length + _modelPaths.Length;
    }

    public Progress LoadNext()
    {
        if (_index < _soundPaths.Length)
        {
            var sound = LoadSound(_soundPaths[_index]);
            Sounds.Add(FormatFileName(_soundPaths[_index]), sound);
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index++ };
        }

        if (_index - _soundPaths.Length < _texturePaths.Length)
        {
            var texture = LoadTexture(_texturePaths[_index]);
            Textures.Add(FormatFileName(_texturePaths[_index]), texture);
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index++ };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length < _modelPaths.Length)
        {
            var model = LoadModel(_modelPaths[_index]);
            Models.Add(FormatFileName(_modelPaths[_index]), model);
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index++ };
        }

        return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
    }

    private string FormatFileName(string fileName)
    {
        return fileName.Split(".")[0];
    }
}

public class Progress
{
    public int TotalFiles { get; set; }
    public int FilesLoaded { get; set; }
}