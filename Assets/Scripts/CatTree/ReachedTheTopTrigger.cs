using UnityEngine;

public class ReachedTheTopTrigger : MonoBehaviour
{
    [SerializeField] private CatTree controller;

    private void OnValidate()
    {
        if (!controller)
        {
            controller = FindObjectOfType<CatTree>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        controller.OnReachedTop();
    }
}
