using UnityEngine;

public class Fadable : MonoBehaviour
{
    [SerializeField] private Renderer theRenderer;
    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material fadedMaterial;

    private void OnValidate()
    {
        if (!theRenderer)
        {
            theRenderer = GetComponent<Renderer>();

            if (!regularMaterial)
                regularMaterial = theRenderer.sharedMaterial;
        }
    }

    public void Fade()
    {
        theRenderer.sharedMaterial = fadedMaterial;
    }

    public void Unfade()
    {
        theRenderer.sharedMaterial = regularMaterial;
    }
}
