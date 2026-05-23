using System.Numerics;

namespace VSurvival.Code.Utils;

public sealed class SnapToGridComponent
{
    [Property]
    public float GridSize { get; set; } = 0.25f;

    [Property]
    public bool SnapY { get; set; }

    public Vector3 Snap(Vector3 source)
    {
        var grid = GridSize <= 0f ? 0.25f : GridSize;
        var snappedX = RoundToGrid(source.X, grid);
        var snappedY = SnapY ? RoundToGrid(source.Y, grid) : source.Y;
        var snappedZ = RoundToGrid(source.Z, grid);
        return new Vector3(snappedX, snappedY, snappedZ);
    }

    private static float RoundToGrid(float value, float gridSize)
    {
        return MathF.Round(value / gridSize) * gridSize;
    }
}

public sealed class PropertyAttribute : System.Attribute
{
}
