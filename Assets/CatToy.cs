using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatToy : MonoBehaviour
{
    public float explosionAmount = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GetComponent<Rigidbody>().AddExplosionForce(explosionAmount, collision.contacts[0].point, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 forceDirection = transform.position - other.gameObject.transform.position;
            forceDirection = forceDirection / GetComponent<Rigidbody>().mass;
            GetComponent<Rigidbody>().AddForce(forceDirection * 1000, ForceMode.Impulse);
        }
    }
}
