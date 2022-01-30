using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 oppositeFromCamera = Camera.main.transform.position - transform.position;
        //transform.LookAt(oppositeFromCamera);
        transform.rotation = Quaternion.Euler(0, 45, 0);
    }
}
