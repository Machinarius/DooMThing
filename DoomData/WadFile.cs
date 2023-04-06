using System.Text.Json;

namespace Machinarius.DoomThing.DoomData;

// reference: https://formats.kaitai.io/doom_wad/
// https://ide.kaitai.io/#
public class WadFile {
  public int NumberOfDirectories { get; }
  public int DirectoryOffset { get; }

  public readonly WadDirectory[] Directories;

  public WadFile(int numberOfDirectories, int directoryOffset) {
    NumberOfDirectories = numberOfDirectories;
    DirectoryOffset = directoryOffset;
    Directories = new WadDirectory[numberOfDirectories];
  }

  public void DumpDebugToConsole() {
    Console.WriteLine("WAD File Data");
    Console.WriteLine(JsonSerializer.Serialize(this));
  }
}
