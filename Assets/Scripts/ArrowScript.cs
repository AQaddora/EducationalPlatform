using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public string[] missions;
    public Transform[] missionsLocations;
    public Transform player;
    public GameObject[] conversations;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 posDiff = missionsLocations[Game.GameState.nowMission].position - transform.position;
        posDiff.y = 0;
        transform.rotation = Quaternion.LookRotation(posDiff , Vector3.up);
        transform.position = player.position + Vector3.up * 2.5f;
        if (conversations[0].name == "DoneTalking" && conversations[1].name == "DoneTalking" && Game.GameState.nowMission==0) {
            Game.GameState.nowMission = 1;
        }
    }
}
