using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class QuestionScreen : MonoBehaviour
{
    public static QuestionScreen instance;
    public Text questionText;
    public Text[] answersText;
    private float alpha = 0;
    private Conversation conversation;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show (ConversationComp cc, Conversation conversation) {
        if (cc.answers.Length > 4) {
            Debug.LogError("Max answers length is 4");
            return ;
        }
        this.conversation = conversation;
        questionText.text = cc.text;
        for (int i=0; i<cc.answers.Length; i++) {
            answersText[i].text = cc.answers[i].text;
            answersText[i].transform.parent.gameObject.SetActive (true);
        }
        for (int i=cc.answers.Length; i<4; i++){
            answersText[i].transform.parent.gameObject.SetActive (false);
        }
        if (cc.answers.Length==0) {
            Invoke("showNext", 3);
        }
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().interactable = true;
    }
    
    public void hide () {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().interactable = false;
    }

    public void confirmAnswer (int index) {
        conversation.proceed (index);
        EventSystem.current.SetSelectedGameObject(null);
    }
    void showNext() {
        confirmAnswer(-1);
    }
}
