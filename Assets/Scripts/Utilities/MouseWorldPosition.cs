using UnityEngine;

public class MouseWorldPosition : MonoBehaviour
{
    public static Vector3 GetPosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out var distance))
        {
            return ray.GetPoint(distance);
        }
        else
        {
            return Vector3.zero;
        }
    }
}
