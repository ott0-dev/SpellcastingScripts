using UnityEngine;

public class CurveRangeAttribute : PropertyAttribute
{
    public readonly float minX;
    public readonly float maxX;
    public readonly float minY;
    public readonly float maxY;

    public CurveRangeAttribute(float minX, float maxX, float minY, float maxY)
    {
        this.minX = minX;
        this.maxX = maxX;
        this.minY = minY;
        this.maxY = maxY;
    }
}