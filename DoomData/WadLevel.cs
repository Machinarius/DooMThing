using System.Numerics;

namespace Machinarius.DoomThing.DoomData;

public class WadLevel {
  private readonly WadReader wadReader;

  public string Name { get; }
  public WadDirectory Directory { get; }
  public int DirectoryIndex { get; }

  public LinkedDirectories DataDirectories { get; }

  public WadLevel(string name, WadDirectory directory, int directoryIndex, WadFile wadFile, WadReader wadReader) {
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
    }

    Name = name;
    Directory = directory ?? throw new ArgumentNullException(nameof(directory));
    DirectoryIndex = directoryIndex;
    DataDirectories = new LinkedDirectories(wadFile, directoryIndex);

    this.wadReader = wadReader ?? throw new ArgumentNullException(nameof(wadReader));
  }

  public Vector2[] Vertexes => DataDirectories.Vertexes.GetData(wadReader.ReadVertex, 4);
  public WadLineDefinition[] LineDefinitions => DataDirectories.LineDefinitions.GetData(wadReader.ReadLineDefinition, 14);

  public class LinkedDirectories {
    private readonly WadFile wadFile;
    private readonly int baseIndex;

    public WadDirectory Things => wadFile.Directories[baseIndex + 1];
    public WadDirectory LineDefinitions => wadFile.Directories[baseIndex + 2];
    public WadDirectory SideDefinitions => wadFile.Directories[baseIndex + 3];
    public WadDirectory Vertexes => wadFile.Directories[baseIndex + 4];
    public WadDirectory Segments => wadFile.Directories[baseIndex + 5];
    public WadDirectory SubSectors => wadFile.Directories[baseIndex + 6];
    public WadDirectory Nodes => wadFile.Directories[baseIndex + 7];
    public WadDirectory Sectors => wadFile.Directories[baseIndex + 8];
    public WadDirectory Reject => wadFile.Directories[baseIndex + 9];
    public WadDirectory BlockMap => wadFile.Directories[baseIndex + 10];

    public LinkedDirectories(WadFile wadFile, int baseIndex) {
      this.wadFile = wadFile ?? throw new ArgumentNullException(nameof(wadFile));
      this.baseIndex = baseIndex;
    }
  }
}
