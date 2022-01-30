using UnityEngine;

public class CatTreeCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 boundsMin;
    [SerializeField] private Vector3 boundsMax;

    private void OnValidate()
    {
        if (target)
        {
            UpdatePosition();
        }
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    [ContextMenu("Update position")]
    private void UpdatePosition()
    {
        Vector3 newPos = target.position + offset;

        newPos = new Vector3(
            Mathf.Clamp(newPos.x, boundsMin.x, boundsMax.x),
            Mathf.Clamp(newPos.y, boundsMin.y, boundsMax.y),
            Mathf.Clamp(newPos.z, boundsMin.z, boundsMax.z));

        transform.position = newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 center = (boundsMin + boundsMax) / 2f;
        Vector3 size = boundsMax - boundsMin;

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);
    }
}
