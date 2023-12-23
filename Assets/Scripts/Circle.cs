using UnityEngine;

public struct Circle
{
    public float radius;
    public Vector2 center;

    public Circle(float radius, Vector2 center)
    {
        this.radius = radius;
        this.center = center;
    }
    public Circle(float radius, float centerX, float centerY)
    {
        this.radius = radius;
        this.center = new Vector2(centerX, centerY);
    }
}

