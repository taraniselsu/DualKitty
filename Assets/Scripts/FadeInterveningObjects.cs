using System.Collections.Generic;
using UnityEngine;

public class FadeInterveningObjects : MonoBehaviour
{
    [SerializeField] private Camera theCamera;
    [SerializeField] private Transform target;

    private readonly List<Fadable> previousObjects = new List<Fadable>();

    private void OnValidate()
    {
        if (!theCamera)
        {
            theCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        UnfadePreviousObjects();
        FadeObjects();
    }

    private void UnfadePreviousObjects()
    {
        foreach (Fadable fadable in previousObjects)
        {
            fadable.Unfade();
        }

        previousObjects.Clear();
    }

    private void FadeObjects()
    {
        Vector3 deltaPos = target.position - theCamera.transform.position;
        Vector3 dir = deltaPos.normalized;
        float maxDistance = deltaPos.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(theCamera.transform.position, dir, maxDistance);
        foreach (RaycastHit hit in hits)
        {
            GameObject go = hit.collider.gameObject;
            Fadable fadable = go.GetComponent<Fadable>();
            if (fadable)
            {
                fadable.Fade();
                previousObjects.Add(fadable);
            }
        }
    }
}
