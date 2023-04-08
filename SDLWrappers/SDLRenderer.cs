using System;
using System.Drawing;
using System.Numerics;
using SDL2;
using SkiaSharp;

namespace Machinarius.DoomThing.SDLWrappers;

public class SDLRenderer : IDisposable {
  public static implicit operator nint(SDLRenderer renderer) => renderer.WindowHandle;

  public readonly nint WindowHandle;

  private readonly nint glContextHandle;
  private readonly GRGlInterface glInterface;
  private readonly GRContext grContext;
  private readonly GRBackendRenderTarget skiaTarget;
  private readonly SKSurface skiaSurface;

  public SDLRenderer(SDLWindow window) {
    WindowHandle = SDL.SDL_CreateRenderer(window.Handle, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
    glContextHandle = SDL.SDL_GL_CreateContext(WindowHandle);
    SDLUtils.EnsureSDLResultIsOk(SDL.SDL_GL_MakeCurrent(WindowHandle, glContextHandle), nameof(SDL.SDL_GL_MakeCurrent));

    var (windowWidth, windowHeight) = window.GetSize();
    glInterface = GRGlInterface.CreateOpenGl((name) => {
      var procAddress = SDL.SDL_GL_GetProcAddress(name);
      Console.WriteLine($"Returning {procAddress} for name {name}");
      return procAddress;
    });
    if (glInterface == null || !glInterface.Validate()) {
      throw new InvalidOperationException("Could not create a GRGLInterface");
    }

    grContext = GRContext.CreateGl(glInterface);
    skiaTarget = new GRBackendRenderTarget(windowWidth, windowHeight, 0, 8, new GRGlFramebufferInfo());
    skiaSurface = SKSurface.Create(grContext, skiaTarget, GRSurfaceOrigin.TopLeft, SKColorType.Rgba8888);
  }

  public void SetDrawColor(Color color) {
    SDLUtils.EnsureSDLResultIsOk(
      SDL.SDL_SetRenderDrawColor(WindowHandle, color.R, color.G, color.B, color.A), 
      nameof(SDL.SDL_SetRenderDrawColor)
    );
  }
  
  public void DrawLine(Vector2 start, Vector2 end) {
    SDLUtils.EnsureSDLResultIsOk(
      SDL.SDL_RenderDrawLineF(WindowHandle, start.X, start.Y, end.X, end.Y),
      nameof(SDL.SDL_RenderDrawLineF)
    );
  }

  public void RenderToScreen() {
    SDL.SDL_RenderPresent(WindowHandle);
  }

  public void ClearScreen() {
    SDLUtils.EnsureSDLResultIsOk(
      SDL.SDL_RenderClear(WindowHandle), nameof(SDL.SDL_RenderClear)
    );
  }

  public void DrawCircle() {
    // Implement Brasenham's algo for circles!
    // https://github.com/pygame/pygame/blob/main/src_c/draw.c#L726
    throw new NotImplementedException();
  }

  public void Dispose() {
    skiaSurface.Dispose();
    skiaTarget.Dispose();
    grContext.Dispose();
    glInterface.Dispose();
    SDL.SDL_GL_DeleteContext(glContextHandle);
    SDL.SDL_DestroyRenderer(WindowHandle);
  }
}
