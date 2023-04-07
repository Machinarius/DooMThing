using SDL2;

namespace Machinarius.DoomThing.SDLWrappers;

public class SDLClock {
  public ulong LastTickInMillis { get; private set; }
  public ulong DeltaTimeInMillis { get; private set; }

  public static ulong Ticks => SDL.SDL_GetTicks64();

  public void Tick() {
    var tickTime = SDL.SDL_GetTicks64();
    DeltaTimeInMillis = tickTime - LastTickInMillis;
    LastTickInMillis = tickTime;
  }
}
