using Machinarius.DoomThing.DoomData;
using Machinarius.DoomThing.SDLWrappers;
using SDL2;

namespace Machinarius.DoomThing.Engine;

public class DoomEngine : IDisposable {
  public readonly string WadPath;
  private readonly WadReader wadReader;
  private readonly WadFile wadFile;
  private readonly SDLClock clock;
  private readonly SDLRenderer renderer;

  public const string EntryPointLevel = "E1M1";

  private WadLevel? currentLevel;

  public DoomEngine(string wadPath, SDLClock clock, SDLRenderer renderer) {
    WadPath = wadPath;
    wadReader = new WadReader(wadPath);
    
    wadFile = ReadHeader();

    this.clock = clock ?? throw new ArgumentNullException(nameof(clock));
    this.renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
  }

  public void Initialize() {
    ReadDirectories();
    currentLevel = GetLevel(EntryPointLevel);
  }

  public void Update() {

  }

  public void Draw() {
    SDL.SDL_SetRenderDrawColor(renderer.Handle, 255, 0, 0, 255);
    SDL.SDL_RenderDrawLine(renderer.Handle, 0, 480, 320, 0);
    SDL.SDL_RenderDrawLine(renderer.Handle, 640, 480, 320, 0);
    SDL.SDL_RenderDrawLine(renderer.Handle, 0, 480, 640, 480);
    SDL.SDL_RenderPresent(renderer.Handle);

    SDL.SDL_SetRenderDrawColor(renderer.Handle, 0, 0, 0, 255);
    SDL.SDL_RenderClear(renderer.Handle);
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
