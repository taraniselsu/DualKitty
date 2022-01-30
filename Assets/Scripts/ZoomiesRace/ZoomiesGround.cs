using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomiesGround : MonoBehaviour
{
    public ZoomiesManager gameManager;

    [SerializeField] MeshRenderer meshRenderer;

    public void SetMaterial(Material newMaterial) {
        meshRenderer.material = newMaterial;
    }
}
