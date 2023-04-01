using System;
using Machinarius.DoomThing.SDLWrappers;
using SDL2;

namespace Machinarius.DoomThing;

public class EntryPoint {
  public static void Main() {
    SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

    using (var window = new SDLWindow("Simple Triangle", 800, 600)) {
      using var renderer = new SDLRenderer(window);
      SDL.SDL_SetRenderDrawColor(renderer.Handle, 255, 0, 0, 255);
      SDL.SDL_RenderDrawLine(renderer.Handle, 0, 480, 320, 0);
      SDL.SDL_RenderDrawLine(renderer.Handle, 640, 480, 320, 0);
      SDL.SDL_RenderDrawLine(renderer.Handle, 0, 480, 640, 480);
      SDL.SDL_RenderPresent(renderer.Handle);

      var running = true;
      while (running) {
        if (SDL.SDL_PollEvent(out var e) != 0) {
          running = e.type switch {
            SDL.SDL_EventType.SDL_QUIT => false,
            SDL.SDL_EventType.SDL_KEYUP => e.key.keysym.sym != SDL.SDL_Keycode.SDLK_ESCAPE,
            _ => true
          };
        }
      }
    }

    SDL.SDL_Quit();
  }
}
