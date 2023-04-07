namespace Machinarius.DoomThing.DoomData;

public class WadLineDefinition {
  public int StartVertexIndex { get; }
  public int EndVertexIndex { get; }
  public int Flags { get; }
  public int FixtureType { get; }
  public int SectorTag { get; }
  public int FrontSideDefinitionIndex { get; }
  public int BackSideDefinitionIndex { get; }

  public WadLineDefinition(
    int startVertexIndex, int endVertexIndex, int flags, int fixtureType, 
    int sectorTag, int frontSideDefinitionIndex, int backSideDefinitionIndex
  ) {
    StartVertexIndex = startVertexIndex;
    EndVertexIndex = endVertexIndex;
    Flags = flags;
    FixtureType = fixtureType;
    SectorTag = sectorTag;
    FrontSideDefinitionIndex = frontSideDefinitionIndex;
    BackSideDefinitionIndex = backSideDefinitionIndex;
  }
}
