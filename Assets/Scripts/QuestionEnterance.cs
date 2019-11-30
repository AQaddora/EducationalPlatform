/*
* Edited by: Ahmed Qaddora
*/
using UnityEngine;

public class QuestionEnterance : MonoBehaviour
{
	public UIAlpha blackScreen;
	public Transform[] playerTransforms;
	
	//Match, True Or False, MCQ
	[Header("Math, True Or False, MCQ!")]
	public GameObject[] questionsObject;
	Transform player;

	private void OnTriggerEnter(Collider other)
	{
		player = other.transform;
		if (other.gameObject.tag.Equals("Player"))
		{
			blackScreen.FadeInOut();
			Invoke("MovePlayer", blackScreen.duration);
		}
	}

	void LoadMenu()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}

	void MovePlayer()
	{
		int index = Random.Range(1, 3);
		if(/*QuestionsManager.matchQuestions.Count < POLES &&*/ QuestionsManager.tfQuestions.Count < 3 && QuestionsManager.mcqQuestions.Count < 1)
		{
			blackScreen.FadeInOut();
			Invoke("LoadMenu", blackScreen.duration);
		}

		check:
		switch (index)
		{
			case 0:
				if (QuestionsManager.matchQuestions.Count < 1)
				{
					index++;
					goto check;
				}
				break;
			case 1:
				if (QuestionsManager.tfQuestions.Count < 3)
				{
					index++;
					goto check;
				}
				break;
			case 2:
				if (QuestionsManager.mcqQuestions.Count < 1)
				{
					index = 0;
					goto check;
				}
				break;
		}

		player.position = playerTransforms[index].position;
		for (int i = 0; i < questionsObject.Length; i++)
		{
			questionsObject[i].SetActive(false);
		}
		questionsObject[index].SetActive(true);
	}
}
