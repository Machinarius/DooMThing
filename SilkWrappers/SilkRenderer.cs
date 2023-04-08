using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkiaSharp;
using System.Drawing;
using System.Numerics;

namespace Machinarius.DoomThing.Platform;

public class SilkRenderer : IGraphicsRenderer {
  public SilkRenderer(IWindow window, GL glContext, Glfw glfw) {
    var grGlInterface = GRGlInterface.CreateOpenGl((name) => {
      var procAddress = glfw.GetProcAddress(name);
      return procAddress;
    });
    if (grGlInterface == null) {
      throw new InvalidOperationException("Could not create a GLFW GRGL context");
    }

    throw new NotImplementedException();
  }

  public void ClearScreen() {
    throw new NotImplementedException();
  }

  public void DrawCircle() {
    throw new NotImplementedException();
  }

  public void DrawLine(Vector2 start, Vector2 end) {
    throw new NotImplementedException();
  }

  public void RenderToScreen() {
    throw new NotImplementedException();
  }

  public void SetDrawColor(Color color) {
    throw new NotImplementedException();
  }
}
