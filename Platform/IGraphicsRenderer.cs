using System.Drawing;
using System.Numerics;

namespace Machinarius.DoomThing.Platform;

public interface IGraphicsRenderer {
  public void SetDrawColor(Color color);
  public void DrawLine(Vector2 start, Vector2 end);
  public void RenderToScreen();
  public void ClearScreen();
  public void DrawCircle();
}
