using Silk.NET.Windowing;

namespace Machinarius.DoomThing.SilkWrappers;

public static class SilkWindowFactory {
  public static IWindow Create(string title, int width, int height) {
    var options = WindowOptions.Default;
    options.Title = title;
    options.Size = new Silk.NET.Maths.Vector2D<int>(width, height);
    return Window.Create(options);
  }
}
