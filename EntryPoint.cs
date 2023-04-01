using System;
using SDL2;

namespace Machinarius.DoomThing;
public class EntryPoint
{
  public static void Main()
  {
    SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

    var window = SDL.SDL_CreateWindow(
        "SDL Triangle",
        SDL.SDL_WINDOWPOS_CENTERED,
        SDL.SDL_WINDOWPOS_CENTERED,
        800,
        600,
        SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN
    );

    var renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

    SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);
    SDL.SDL_RenderDrawLine(renderer, 0, 480, 320, 0);
	  SDL.SDL_RenderDrawLine(renderer, 640, 480, 320, 0);
	  SDL.SDL_RenderDrawLine(renderer, 0, 480, 640, 480);
    SDL.SDL_RenderPresent(renderer);

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

    SDL.SDL_DestroyRenderer(renderer);
    SDL.SDL_DestroyWindow(window);
    SDL.SDL_Quit();
  }
}
