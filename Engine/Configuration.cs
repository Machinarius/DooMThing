namespace Machinarius.DoomThing.Engine;

public static class Configuration {
  private const int DoomWidth = 320;
  private const int DoomHeight = 200;

  public const double ScaleFactor = 5.0;

  public static int WindowHeight => (int)Math.Floor(DoomHeight * ScaleFactor);
  public static int WindowWidth => (int)Math.Floor(DoomWidth * ScaleFactor);
}