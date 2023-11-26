using UnityEngine;

public static class CameraExtensions
{
    public static Boundary GetBoundaries(this Camera camera)
    {
        Vector2 topRight = camera.ViewportToWorldPoint(new Vector2(1f, 1f));
        Vector2 bottomLeft = camera.ViewportToWorldPoint(new Vector2(0f, 0f));

        Boundary boundary = new(
            left: bottomLeft.x,
            right: topRight.x,
            top: topRight.y,
            bottom: bottomLeft.y);

        return boundary;
    }
}
