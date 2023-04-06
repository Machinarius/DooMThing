namespace Machinarius.DoomThing.DoomData;

public class WadDirectory {
  public int Offset { get; }
  public int SizeInBytes { get; }
  public string Name { get; }
  public bool IsVirtual => SizeInBytes == 0;

  public WadDirectory(int offset, int sizeInBytes, string name) {
    if (string.IsNullOrEmpty(name)) {
      throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
    }

    Offset = offset;
    SizeInBytes = sizeInBytes;
    Name = name;
  }

  public TData[] GetData<TData>(Func<int, TData> reader, int strideInBytes, int headerLength = 0) {
    var itemCount = (int)Math.Floor((double)(SizeInBytes / strideInBytes));
    var dataItems = Enumerable
      .Range(0, itemCount)
      .Select((itemOffset) => 
        reader(Offset + itemOffset * strideInBytes + headerLength)
      ).ToArray();
    return dataItems;
  }
}