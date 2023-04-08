using Machinarius.DoomThing.DoomData;
using Machinarius.DoomThing.Platform;
using System.Drawing;

namespace Machinarius.DoomThing.Engine;

public class DoomEngine : IDisposable {
  public readonly string WadPath;
  private readonly WadReader wadReader;
  private readonly WadFile wadFile;
  public readonly IGraphicsRenderer Renderer;

  public const string EntryPointLevel = "E1M1";

  public WadLevel? CurrentLevel { get; private set; }
  private MapRenderer? mapRenderer;

  public DoomEngine(string wadPath, IGraphicsRenderer renderer) {
    WadPath = wadPath;
    wadReader = new WadReader(wadPath);
    Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
    
    wadFile = ReadHeader();
  }

  public void Initialize() {
    ReadDirectories();
    CurrentLevel = GetLevel(EntryPointLevel);
    mapRenderer = new MapRenderer(this);
  }

  public void Update() {

  }

  public void Draw() {
    mapRenderer?.DrawVertexes();
    Renderer.RenderToScreen();

    Renderer.SetDrawColor(Color.Black);
    Renderer.ClearScreen();
  }

  private WadFile ReadHeader() {
    var header = wadReader.ReadString(0, 4);
    if (header != "IWAD" && header != "PWAD") {
      throw new InvalidOperationException($"The provided WAD type ${header} doesn't match the expected type");
    }

    return new WadFile(wadReader.ReadInt(4), wadReader.ReadInt(8));
  }

  private void ReadDirectories() {
    for (var i = 0; i < wadFile.NumberOfDirectories; i++) {
      var offset = wadFile.DirectoryOffset + (i * 16);
      var directory = new WadDirectory(wadReader.ReadInt(offset), wadReader.ReadInt(offset + 4), wadReader.ReadString(offset + 8, 8));
      wadFile.Directories[i] = directory;
    }
  }

  private WadLevel GetLevel(string levelName) {
    int levelIndex = 0;
    WadDirectory? levelDirectory = null;
    for (int i = 0; i < wadFile.Directories.Length; i++) {
      if (wadFile.Directories[i].Name == levelName) {
        levelIndex = i;
        levelDirectory = wadFile.Directories[i];
        break;
      }
    }

    if (levelDirectory == null) {
      throw new Exception("Could not find a WAD Directory for level " + levelName);
    }

    return new WadLevel(levelName, levelDirectory, levelIndex, wadFile, wadReader);
  }

  public void Dispose() {
    wadReader.Dispose();
  }
}
