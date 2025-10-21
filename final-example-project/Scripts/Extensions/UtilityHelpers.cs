using Godot;

public static class UtilityHelpers
{
    /// <summary>
    /// Converts a Vector3 to a Vector2 by ignoring the Y component.
    /// </summary>
    /// <param name="vector">The Vector3 to convert.</param>
    /// <returns>A Vector2 with the X and Z components of the original Vector3.</returns>
    public static Vector3 WithNewY(this Vector3 vector, float newY)
    {
        return new Vector3(vector.X, newY, vector.Z);
    }
}