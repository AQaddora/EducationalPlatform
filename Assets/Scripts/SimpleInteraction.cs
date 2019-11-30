using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class SimpleInteraction : MonoBehaviour
{
    public bool stopPlayer = false;
    public string question = "";
    public string[] answers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            Game.GameState.controllable = !stopPlayer;
            //QuestionScreen.show (question, answers);
        }
    }
}
