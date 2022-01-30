using UnityEngine;

public class ZoomyModeBeforeRace : MonoBehaviour
{
    float moveSpeed = 0f;
    bool zoomsOn = false;

    [SerializeField] float initialMoveSpeed = 4.0f;
    [SerializeField] GameObject node1;
    [SerializeField] GameObject node2;
    [SerializeField] GameObject node3;
    [SerializeField] GameObject node4;
    int nodeToRideTo = 1;

    [SerializeField] Animator kittyAnimator;

    void Update()
    {
        if (zoomsOn)
        {
            Vector3 oldPos = transform.position;

            switch (nodeToRideTo)
            {
                case 1:
                    transform.position = Vector3.MoveTowards(transform.position, node1.transform.position, Time.deltaTime * moveSpeed);
                    if (transform.position == node1.transform.position) nodeToRideTo = 2;
                    break;
                case 2:
                    transform.position = Vector3.MoveTowards(transform.position, node2.transform.position, Time.deltaTime * moveSpeed);
                    if (transform.position == node2.transform.position) nodeToRideTo = 3;
                    break;
                case 3:
                    transform.position = Vector3.MoveTowards(transform.position, node3.transform.position, Time.deltaTime * moveSpeed);
                    if (transform.position == node3.transform.position) nodeToRideTo = 4;
                    break;
                case 4:
                    transform.position = Vector3.MoveTowards(transform.position, node4.transform.position, Time.deltaTime * moveSpeed);
                    if (transform.position == node4.transform.position) nodeToRideTo = 1;
                    break;
            }

            Vector3 deltaMovement = (transform.position - oldPos).normalized * moveSpeed;
            kittyAnimator.SetFloat("xMove", deltaMovement.x);
            kittyAnimator.SetFloat("zMove", deltaMovement.z);

            moveSpeed += Time.deltaTime * 6;
        }
    }

    public void TriggerZooms()
    {
        zoomsOn = true;
        moveSpeed = initialMoveSpeed;
    }
}
