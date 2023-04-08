using Silk.NET.GLFW;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SkiaSharp;
using System.Drawing;
using System.Numerics;

namespace Machinarius.DoomThing.Platform;

public class SilkRenderer : IGraphicsRenderer {
  public SilkRenderer(IWindow window, GL glContext) {
    if (!window.GLContext.IsCurrent) {
      window.GLContext.MakeCurrent();
    }

    var grGlInterface = GRGlInterface.Create((name) => {
      if (!window.GLContext.TryGetProcAddress(name, out var procAddress)) {
        procAddress = IntPtr.Zero;
      }
      Console.WriteLine($"Returning address {procAddress} for proc {name}");
      return procAddress;
    });
    if (grGlInterface == null || !grGlInterface.Validate()) {
      throw new InvalidOperationException("Could not create a valid GRGL context");
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
