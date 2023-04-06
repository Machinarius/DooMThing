namespace Machinarius.DoomThing.DoomData;

public class WadDirectory {
  public int Offset { get; }
  public int Size { get; }
  public string Name { get; }

  public WadDirectory(int offset, int size, string name) {
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
    }

    Offset = offset;
    Size = size;
    Name = name;
  }
}