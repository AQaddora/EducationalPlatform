using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ConversationComp
{
    public string text;
    public Answer[] answers;

}
[System.Serializable]
public class Answer
{
    public string text;
    public int nextState;
}
