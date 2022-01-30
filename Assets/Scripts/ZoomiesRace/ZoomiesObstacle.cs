using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomiesObstacle : MonoBehaviour
{
    public ZoomiesManager gameManager;
    [SerializeField] Transform logicalTransform;
    [SerializeField] Transform physicalTransform;

    // Assumed: Freeze Position is unticked for all 3 axes
    // Assumed: Freeze Rotation is ticked for all 3 axes
    // Assumed: Kinematic is unticked. We'll probably tick it via code. This is
    // to ensure that the code can position the block exactly, whereas kinematic
    // objects don't always get positioned exactly.
    [SerializeField] new Rigidbody rigidbody;

    [SerializeField] new Collider collider;

}
