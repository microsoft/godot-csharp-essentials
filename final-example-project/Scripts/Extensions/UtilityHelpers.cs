using Godot;

public static class UtilityHelpers
{
    /// <summary>
    /// Creates a new Vector3 with the same X and Z components but a different Y value.
    /// </summary>
    /// <param name="vector">The Vector3 to modify.</param>
    /// <param name="newY">The new Y value to use.</param>
    /// <returns>A new Vector3 with the original X and Z components and the specified Y value.</returns>
    public static Vector3 WithNewY(this Vector3 vector, float newY)
    {
        return new Vector3(vector.X, newY, vector.Z);
    }
}