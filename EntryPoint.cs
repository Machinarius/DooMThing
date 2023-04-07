using System;
using Machinarius.DoomThing.Engine;
using Machinarius.DoomThing.SDLWrappers;
using SDL2;

namespace Machinarius.DoomThing;

public class EntryPoint {
  public static void Main() {
    SDLInitializer.Initialize();

    var clock = new SDLClock();
    using (var window = new SDLWindow("Simple Triangle", Configuration.WindowWidth, Configuration.WindowHeight)) {
      using var renderer = new SDLRenderer(window);
      var engine = new DoomEngine(Path.Combine(".", "Data", "DOOM1.WAD"), clock, renderer);
      engine.Initialize();

      var running = true;
      while (running) {
        clock.Tick();
        engine.Update();
        engine.Draw();
        running = ShouldGameStillRun();
      }
    }

    SDL.SDL_Quit();
  }

  static private bool ShouldGameStillRun() {
    var running = true;
    if (SDL.SDL_PollEvent(out var sdlEvent) != 0) {
      running = sdlEvent.type switch {
        SDL.SDL_EventType.SDL_QUIT => false,
        SDL.SDL_EventType.SDL_KEYUP => sdlEvent.key.keysym.sym != SDL.SDL_Keycode.SDLK_ESCAPE,
        _ => true
      };
    }

    return running;
  }
}
