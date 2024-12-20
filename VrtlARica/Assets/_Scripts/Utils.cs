using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = 0;
        return camera.ScreenToWorldPoint(position);
    }
}
