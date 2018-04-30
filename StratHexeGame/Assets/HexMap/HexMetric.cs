using UnityEngine;

public static class HexMetric {
    public const float OUTERRADIUS = 10f;

    public const float INNERRADIUS = OUTERRADIUS * 0.866025404f;

    public const float solidFactor = 0.75f;

    public const float blendFactor = 1f - solidFactor;

    public const float elevationStep = 5f;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, OUTERRADIUS),
        new Vector3(INNERRADIUS, 0f, 0.5f * OUTERRADIUS),
        new Vector3(INNERRADIUS, 0f, -0.5f * OUTERRADIUS),
        new Vector3(0f, 0f, -OUTERRADIUS),
        new Vector3(-INNERRADIUS, 0f, -0.5f * OUTERRADIUS),
        new Vector3(-INNERRADIUS, 0f, 0.5f * OUTERRADIUS),
        new Vector3(0f, 0f, OUTERRADIUS)
    };

    public static Vector3 GetFirstCorner(HexDirection direction)
    {
        return corners[(int)direction];
    }

    public static Vector3 GetSecondCorner(HexDirection direction)
    {
        return corners[(int)direction + 1];
    }

    public static Vector3 GetFirstSolidCorner(HexDirection direction)
    {
        return corners[(int)direction] * solidFactor;
    }

    public static Vector3 GetSecondSolidCorner(HexDirection direction)
    {
        return corners[(int)direction + 1] * solidFactor;
    }

    public static Vector3 GetBridge(HexDirection direction)
    {
        return (corners[(int)direction] + corners[(int)direction + 1]) *
               blendFactor;
    }
}
