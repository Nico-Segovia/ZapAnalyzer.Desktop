namespace ZapAnalyzer.Desktop.Models;

/// <summary>
/// Define un prisma rectangular alineado a los ejes cartesianos (AABB).
/// </summary>
public class ThreeDObject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Category { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Depth { get; set; }

    // Proyecciones máximas del volumen en el espacio 3D
    public double MaxX => X + Width;
    public double MaxY => Y + Height;
    public double MaxZ => Z + Depth;

    /// <summary>
    /// Evalúa la contención total de un volumen externo dentro del actual.
    /// </summary>
    public bool Contains(ThreeDObject other)
    {
        return X <= other.X && MaxX >= other.MaxX &&
               Y <= other.Y && MaxY >= other.MaxY &&
               Z <= other.Z && MaxZ >= other.MaxZ;
    }

    /// <summary>
    /// Evalúa la colisión espacial (intersección) entre dos volúmenes AABB.
    /// </summary>
    public bool IntersectsWith(ThreeDObject other)
    {
        return X < other.MaxX && MaxX > other.X &&
               Y < other.MaxY && MaxY > other.Y &&
               Z < other.MaxZ && MaxZ > other.Z;
    }
}