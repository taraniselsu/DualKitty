using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

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
        transform.position = target.position + offset;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
