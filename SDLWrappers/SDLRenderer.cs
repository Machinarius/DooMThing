using System;
using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public class SDLRenderer : IDisposable {
  public static implicit operator nint(SDLRenderer renderer) => renderer.Handle;

  public readonly nint Handle;

  public SDLRenderer(SDLWindow window) {
    Handle = SDL.SDL_CreateRenderer(window.Handle, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
  }

  public void Dispose() {
    SDL.SDL_DestroyRenderer(Handle);
  }
}
