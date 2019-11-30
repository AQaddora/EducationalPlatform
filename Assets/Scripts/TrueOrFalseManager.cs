using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TrueOrFalseManager : MonoBehaviour
{
	public GameObject controllers;
	public RTLTMPro.RTLTextMeshPro[] questions;
	public static TrueOrFalse[] currentQuestions;
	public GameObject mcqGameObject;

    void OnEnable()
    {
		if(QuestionsManager.tfQuestions.Count < 3)
		{
			QuestionsManager.FindAnotherGame();
			return;
		}

		Debug.Log(QuestionsManager.tfQuestions.Count);
		controllers.SetActive(false);

		currentQuestions = new TrueOrFalse[questions.Length];

		for (int i = 0; i < questions.Length; i++)
		{
			currentQuestions[i] = QuestionsManager.tfQuestions[0];
			QuestionsManager.tfQuestions.RemoveAt(0);
		}
		for (int i = 0; i < questions.Length; i++)
		{
			questions[i].text = (currentQuestions[i].question);
			questions[i].transform.parent.parent.GetComponent<DragMe>().index = i;
			if (currentQuestions[i].answers.Contains("صح"))
			{
				questions[i].transform.parent.parent.tag = "TrueTF";
			}
			else
			{
				questions[i].transform.parent.parent.tag = "False";
			}
		}
    }

	private void OnDisable()
	{
		controllers.SetActive(true);
	}
}
