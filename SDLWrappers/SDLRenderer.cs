using System;
using System.Drawing;
using System.Numerics;
using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public class SDLRenderer : IDisposable {
  public static implicit operator nint(SDLRenderer renderer) => renderer.Handle;

  public readonly nint Handle;

  public SDLRenderer(SDLWindow window) {
    Handle = SDL.SDL_CreateRenderer(window.Handle, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
  }

  public void SetDrawColor(Color color) {
    EnsureSDLResultIsOk(
      SDL.SDL_SetRenderDrawColor(Handle, color.R, color.G, color.B, color.A), 
      nameof(SDL.SDL_SetRenderDrawColor)
    );
  }
  
  public void DrawLine(Vector2 start, Vector2 end) {
    EnsureSDLResultIsOk(
      SDL.SDL_RenderDrawLineF(Handle, start.X, start.Y, end.X, end.Y),
      nameof(SDL.SDL_RenderDrawLineF)
    );
  }

  public void RenderToScreen() {
    SDL.SDL_RenderPresent(Handle);
  }

  public void ClearScreen() {
    EnsureSDLResultIsOk(
      SDL.SDL_RenderClear(Handle), nameof(SDL.SDL_RenderClear)
    );
  }

  static private void EnsureSDLResultIsOk(int sdlResult, string methodName) {
    if (sdlResult == 0) {
      return;
    }

    throw new Exception($"Invoking ${methodName} failed. Result code: " + sdlResult);
  }

  public void Dispose() {
    SDL.SDL_DestroyRenderer(Handle);
  }
}
