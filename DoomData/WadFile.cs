using System.Text.Json;

namespace Machinarius.DoomThing.DoomData;

public class WadFile {
  public int NumberOfDirectories { get; set; }
  public int DirectoryOffset { get; set; }

  public List<WadDirectory> Directories { get; set; } = new List<WadDirectory>();

  public void DumpDebugToConsole() {
    Console.WriteLine("WAD File Data");
    Console.WriteLine(JsonSerializer.Serialize(this));
  }
}
