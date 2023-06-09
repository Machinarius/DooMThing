using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public class SDLWindow : IDisposable {
  public static implicit operator nint(SDLWindow window) => window.Handle;

  public readonly nint Handle;

  public SDLWindow(string title, int width, int height) {
    Handle = SDL.SDL_CreateWindow(
      title,
      SDL.SDL_WINDOWPOS_CENTERED,
      SDL.SDL_WINDOWPOS_CENTERED,
      width,
      height,
      SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL
    );
  }

  public (int Width, int Height) GetSize() {
    SDL.SDL_GetWindowSize(Handle, out var Width, out var Height);
    return (Width, Height);
  }

  public void Dispose() {
    SDL.SDL_DestroyWindow(Handle);
  }
}