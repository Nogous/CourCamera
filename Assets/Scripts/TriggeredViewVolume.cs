using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggeredViewVolume : AViewVolume
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        view.SetActive(true);   
    }

    private void OnTriggerExit(Collider other)
    {
        view.SetActive(false);
    }
}
