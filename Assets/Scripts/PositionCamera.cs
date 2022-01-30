using UnityEngine;

public class PositionCamera : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float distance;

    private void OnValidate()
    {
        transform.localPosition = Quaternion.Euler(rotation) * (Vector3.back * distance);
        transform.LookAt(transform.parent);
    }
}
