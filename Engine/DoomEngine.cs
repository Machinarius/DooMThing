using Machinarius.DoomThing.DoomData;
namespace Machinarius.DoomThing.Engine;

public class DoomEngine : IDisposable {
  public readonly string WadPath;
  private readonly WadReader wadReader;
  private readonly WadFile wadFile;

  public DoomEngine(string wadPath) {
    WadPath = wadPath;
    wadReader = new WadReader(wadPath);
    wadFile = new WadFile();
  }

  public void Initialize() {
    ReadHeader();
    ReadDirectories();
  }

  private void ReadHeader() {
    var header = wadReader.ReadString(0, 4);
    if (header != "IWAD" && header != "PWAD") {
      throw new InvalidOperationException($"The provided WAD type ${header} doesn't match the expected type");
    }

    wadFile.NumberOfDirectories = wadReader.ReadInt(4);
    wadFile.DirectoryOffset = wadReader.ReadInt(8);
  }

  private void ReadDirectories() {
    for (var i = 0; i < wadFile.NumberOfDirectories; i++) {
      var offset = wadFile.DirectoryOffset + (i * 16);
      var directory = new WadDirectory(wadReader.ReadInt(offset), wadReader.ReadInt(offset + 4), wadReader.ReadString(offset + 8, 8));
      wadFile.Directories.Add(directory);
    }
  }

  public void Dispose() {
    wadReader.Dispose();
  }
}
