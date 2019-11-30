using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] people;
    public int count = 30;
    public static PeopleManager instance;
    void Start() {
        instance = this;
        for (int i = 0; i < count; i++) {
            GiveRandomTarget (Instantiate(people[Random.Range(0, people.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity));
        }
    }

    public void GiveRandomTarget (GameObject character) {
        character.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}
