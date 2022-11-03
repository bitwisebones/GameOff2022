
using Raylib_cs;
using System;
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
    private string[] _imagePaths;
    private int _index = 0;
    private int _totalFiles;

    public Dictionary<string, Sound> Sounds { get; } = new Dictionary<string, Sound>();
    public Dictionary<string, Texture2D> Textures { get; } = new Dictionary<string, Texture2D>();
    public Dictionary<string, Model> Models { get; } = new Dictionary<string, Model>();
    public Dictionary<string, Image> Images { get; } = new Dictionary<string, Image>();

    private ResourceManager()
    {
        _soundPaths = Directory.GetFiles("Resources/Audio");
        _texturePaths = Directory.GetFiles("Resources/Textures");
        _modelPaths = Directory.GetFiles("Resources/Models");
        _imagePaths = Directory.GetFiles("Resources/Images");
        _totalFiles = _soundPaths.Length + _texturePaths.Length + _modelPaths.Length + _imagePaths.Length;

        Console.WriteLine($"{_soundPaths.Length}, {_texturePaths.Length}, {_modelPaths.Length}, {_imagePaths.Length}");
    }

    public Progress LoadNext()
    {
        if (_index < _soundPaths.Length - 1)
        {
            var idx = _index;
            var sound = LoadSound(_soundPaths[idx]);
            Sounds.Add(FormatFileName(_soundPaths[idx]), sound);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length < _texturePaths.Length)
        {
            var idx = _index - _soundPaths.Length;
            var texture = LoadTexture(_texturePaths[idx]);
            Textures.Add(FormatFileName(_texturePaths[idx]), texture);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length < _modelPaths.Length)
        {
            var idx = _index - _soundPaths.Length - _texturePaths.Length;
            var model = LoadModel(_modelPaths[idx]);
            Models.Add(FormatFileName(_modelPaths[idx]), model);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        if (_index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length < _imagePaths.Length)
        {
            var idx = _index - _soundPaths.Length - _texturePaths.Length - _modelPaths.Length;
            var image = LoadImage(_imagePaths[idx]);
            Images.Add(FormatFileName(_imagePaths[idx]), image);
            _index++;
            return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
        }

        return new Progress { TotalFiles = _totalFiles, FilesLoaded = _index };
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