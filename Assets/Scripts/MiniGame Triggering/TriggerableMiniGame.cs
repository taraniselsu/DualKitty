using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableMiniGame : MonoBehaviour
{
    [SerializeField] GameObject pressEIndicator;
    public GameObject triggerObject;
    public bool inTrigger = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        pressEIndicator.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TriggerMinigame()
    {
        pressEIndicator.SetActive(false);
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressEIndicator.SetActive(false);
            inTrigger = false;
        }
    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pressEIndicator.SetActive(true);
            inTrigger = true;
            triggerObject = other.gameObject;
            Debug.Log("Trigger with + " + name);
        }
    }
}
