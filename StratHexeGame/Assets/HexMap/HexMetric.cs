using UnityEngine;

public static class HexMetric {
    public const float OUTERRADIUS = 10f;

    public const float INNERRADIUS = OUTERRADIUS * 0.866025404f;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, OUTERRADIUS),
        new Vector3(INNERRADIUS, 0f, 0.5f * OUTERRADIUS),
        new Vector3(INNERRADIUS, 0f, -0.5f * OUTERRADIUS),
        new Vector3(0f, 0f, -OUTERRADIUS),
        new Vector3(-INNERRADIUS, 0f, -0.5f * OUTERRADIUS),
        new Vector3(-INNERRADIUS, 0f, 0.5f * OUTERRADIUS),
        new Vector3(0f, 0f, OUTERRADIUS)
    };
}
