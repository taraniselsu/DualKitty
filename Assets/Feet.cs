using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{

    public FurnitureHuntManager furnitureHuntManager;

    public GameObject rightmostPos;
    public GameObject leftmostPos;
    bool goingLeft = true;

    Vector3 rightMostStartPos;
    Vector3 leftMostStartPos;

    float speed = 1.0f;

    float ouchTime = 0.2f;
    float ouchTimer = 0.0f;
    bool ouch = false;

    // Start is called before the first frame update
    void Start()
    {
        rightMostStartPos = rightmostPos.transform.position;
        leftMostStartPos = leftmostPos.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ouch)
        {
            ouchTimer += Time.deltaTime;
            if (ouchTimer > ouchTime) UnOuch();
        }
        if (goingLeft)
        {
            transform.position = Vector3.MoveTowards(transform.position, leftmostPos.transform.position, speed * Time.deltaTime);
            if (transform.position == leftmostPos.transform.position) goingLeft = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, rightmostPos.transform.position, speed * Time.deltaTime);
            if (transform.position == rightmostPos.transform.position) goingLeft = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Hit feet");
            
            furnitureHuntManager.HitFeet();
            goingLeft = !goingLeft;
            speed += 0.2f;

            float xSize = GetComponent<BoxCollider>().size.x;
            xSize = xSize * .95f;
            GetComponent<BoxCollider>().size = new Vector3(xSize, 1, .2f);
            Ouch();
        }
    }

    // This function makes the feet retreat for 1 second
    void Ouch()
    {
        ouchTimer = 0.0f;
        ouch = true;

        rightmostPos.transform.Translate(Vector3.forward);
        leftmostPos.transform.Translate(Vector3.forward);
    }

    void UnOuch()
    {
        ouch = false;

        rightmostPos.transform.position = rightMostStartPos + Vector3.right * .05f;
        leftmostPos.transform.position = leftMostStartPos + Vector3.left * .05f;
        rightMostStartPos = rightmostPos.transform.position;
        leftMostStartPos = leftmostPos.transform.position;
    }
}
