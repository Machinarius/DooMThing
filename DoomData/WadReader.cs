using SDL2;
using System.Numerics;

namespace Machinarius.DoomThing.DoomData;

public class WadReader: IDisposable {
  private readonly FileStream wadStream;
  private readonly StreamReader streamReader;

  public WadReader(string path) {
    wadStream = File.OpenRead(path);
    streamReader = new StreamReader(wadStream);
  }

  public string ReadString(int offset, int stringLength) {
    var buffer = new byte[stringLength];

    wadStream.Seek(offset, SeekOrigin.Begin);
    if (wadStream.Read(buffer, 0, stringLength)  != stringLength) {
      throw new InvalidOperationException($"Could not read the string of length ${stringLength} you requested");
    }

    var trimmedBuffer = buffer.TakeWhile((b) => b != 0).ToArray();
    var result = System.Text.Encoding.ASCII.GetString(trimmedBuffer);
    return result;
  }

  public int ReadInt(int offset) {
    wadStream.Seek(offset, SeekOrigin.Begin);
    var intBuffer = new byte[4];
    if (wadStream.Read(intBuffer, 0, 4) != 4) {
      throw new InvalidOperationException($"Could not read 4 bytes for an int you requested");
    }
    var result = BitConverter.ToInt32(intBuffer);
    return result;
  }

  public short ReadShort(int offset) {
    wadStream.Seek(offset, SeekOrigin.Begin);
    var shortBuffer = new byte[2];
    if (wadStream.Read(shortBuffer, 0, 2) != 2) {
      throw new InvalidOperationException($"Could not read 2 bytes for a short you requested");
    }
    var result = BitConverter.ToInt16(shortBuffer);
    return result;
  }

  public byte ReadByte(int offset) {
    wadStream.Seek(offset, SeekOrigin.Begin);
    var result = wadStream.ReadByte();
    return (byte)result;
  }

  public Vector2 ReadVertex(int offset) {
    var x = ReadShort(offset);
    var y = ReadShort(offset + 2);
    return new Vector2(x, y);
  }

  public WadLineDefinition ReadLineDefinition(int offset) {
    var start = ReadShort(offset);
    var end = ReadShort(offset + 2);
    var flags = ReadShort(offset + 4);
    var type = ReadShort(offset + 6);
    var sector = ReadShort(offset + 8);
    var frontSide = ReadShort(offset + 10);
    var backSide = ReadShort(offset + 12);
    return new WadLineDefinition(
      start, end, flags, type, sector, frontSide, backSide
    );
  }

  public void Dispose() {
    streamReader.Close();
    streamReader.Dispose();
  }
}
