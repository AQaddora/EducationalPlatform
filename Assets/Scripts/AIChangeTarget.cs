using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChangeTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if (other.tag == "SpawnPoint") {
            PeopleManager.instance.GiveRandomTarget(gameObject);
        }
    }
}
