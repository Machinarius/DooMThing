using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public static class SDLUtils {
  public static void EnsureSDLResultIsOk(int sdlResult, string methodName) {
    if (sdlResult == 0) {
      return;
    }

    var sdlMessage = SDL.SDL_GetError();
    throw new Exception($"Invoking {methodName} failed. Result code: {sdlResult}, Error message: {sdlMessage}");
  }
}
