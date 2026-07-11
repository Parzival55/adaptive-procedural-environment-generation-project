using UnityEngine;
public class CameraFocus : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        if (target == null)
            return;

        transform.LookAt(target);
    }
}