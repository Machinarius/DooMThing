using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public static class SDLInitializer {
  public static void Initialize() {
    var initResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_TIMER);
    if (initResult != 0) {
      throw new Exception("Could not initialize SDL with video and timers. Result: " + initResult);
    }
  }
}
