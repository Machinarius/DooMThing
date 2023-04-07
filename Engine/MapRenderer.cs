using Machinarius.DoomThing.DoomData;
using System.Drawing;
using System.Numerics;

namespace Machinarius.DoomThing.Engine;

public class MapRenderer {
  private readonly DoomEngine engine;
  private readonly Vector2[] mapVertexes;
  private readonly WadLineDefinition[] lineDefs;

  public MapRenderer(DoomEngine engine) {
    this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
    (mapVertexes, lineDefs) = LoadMapData();
  }

  private (Vector2[], WadLineDefinition[]) LoadMapData() {
    var level = engine.CurrentLevel;
    if (level == null) {
      throw new Exception("Can not render a map when the engine has no level");
    }

    var vertexes = level.Vertexes;
    var sortedByX = vertexes.OrderBy(vertex => vertex.X);
    var (minimumX, maximumX) = (sortedByX.First().X, sortedByX.Last().X);
    var sortedByY = vertexes.OrderBy(vertex => vertex.Y);
    var (minimumY, maximumY) = (sortedByY.First().Y, sortedByY.Last().Y);

    var paddingLeft = Configuration.Padding;
    var paddingRight = Configuration.WindowWidth - Configuration.Padding;
    var paddingTop = Configuration.Padding;
    var paddingBottom = Configuration.WindowHeight - Configuration.Padding;

    var remappedVertexes = vertexes.Select(vertex => {
      return new Vector2(
        (Math.Max(minimumX, Math.Min(vertex.X, maximumX)) - minimumX) * (paddingRight - paddingLeft) / (maximumX - minimumX) + paddingLeft,
        Configuration.WindowHeight - (Math.Max(minimumY, Math.Min(vertex.Y, maximumY)) - minimumY) * (paddingBottom - paddingTop) / (maximumY - minimumY) - paddingTop 
      );
    }).ToArray();

    return (remappedVertexes, level.LineDefinitions);
  }



  public void DrawVertexes() {
    engine.Renderer.SetDrawColor(Color.LimeGreen);
    foreach(var line in lineDefs) {
      var startVertex = mapVertexes[line.StartVertexIndex];
      var endVertex = mapVertexes[line.EndVertexIndex];
      engine.Renderer.DrawLine(startVertex, endVertex);
    }
  }
}
