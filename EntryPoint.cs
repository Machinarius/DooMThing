using Machinarius.DoomThing.Engine;
using Machinarius.DoomThing.Platform;
using Machinarius.DoomThing.SilkWrappers;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Machinarius.DoomThing;

public class EntryPoint {
  public static void Main() {
    DoomEngine? engine = null;
    GL? glContext = null;
    IInputContext? inputContext = null;

    using var window = SilkWindowFactory.Create("Simple Doom Parser", Configuration.WindowWidth, Configuration.WindowHeight);
    window.Load += () => {
      glContext = window.CreateOpenGL();
      inputContext = window.CreateInput();

      var renderer = new SilkRenderer(window, glContext);
      engine = new DoomEngine(Path.Combine(".", "Data", "DOOM1.WAD"), renderer);
      engine.Initialize();  
    };

    window.FramebufferResize += size => {
      glContext?.Viewport(size);
    };

    window.Render += deltaTime => {
      engine?.Update();
      engine?.Draw();

      glContext?.ClearColor(System.Drawing.Color.Wheat);
      glContext?.Clear(ClearBufferMask.ColorBufferBit);
      if (!ShouldGameStillRun(inputContext)) {
        window.Close();
      }
    };

    window.Closing += () => {
      //glfw?.Dispose();
      glContext?.Dispose();
      inputContext?.Dispose();
    };

    window.Run();
  }

  static private bool ShouldGameStillRun(IInputContext? inputContext) {
    if (inputContext == null || inputContext.Keyboards.Count < 1) {
      // Can't ESC out when there are no keyboards ¯\_(ツ)_/¯
      return true;
    }

    if (inputContext.Keyboards[0].IsKeyPressed(Key.Escape)) {
      return false;
    }

    return true;
  }
}
