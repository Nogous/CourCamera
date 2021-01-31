using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{
    public Vector3 respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        other.transform.position = respawnPoint;
    }
}
