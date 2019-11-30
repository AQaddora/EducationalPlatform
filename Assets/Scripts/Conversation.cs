using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Conversation : MonoBehaviour
{
    public bool stopPlayer = false;
    public ConversationComp[] conversationList;
    public int characterPersona ;
    public int conversationState = 0; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && conversationState != -1 && Game.GameState.characterPersona == characterPersona) {
            Game.GameState.controllable = !stopPlayer;
            QuestionScreen.instance.show (conversationList[conversationState], this);
        }
    }
    public void proceed (int index) {
        if (index != -1)
            conversationState = conversationList[conversationState].answers[index].nextState;
        else
            conversationState++;
        if (conversationState == conversationList.Length) conversationState = -1;
        if (conversationState == -1) {
            gameObject.name = "DoneTalking";
            QuestionScreen.instance.hide ();
            Game.GameState.controllable = true;
            return ;   
        }
        QuestionScreen.instance.show (conversationList[conversationState], this);
        
    }
}
