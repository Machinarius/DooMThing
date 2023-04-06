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
      throw new InvalidOperationException($"Could not read 4 bytes for an int as you requested");
    }
    var result = BitConverter.ToInt32(intBuffer);
    return result;
  }

  public void Dispose() {
    streamReader.Close();
    streamReader.Dispose();
  }
}
