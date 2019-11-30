using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ArabicSupport;

public class MCQ_Manager : MonoBehaviour
{
	public static MCQ question;

	[Space]
	[Header("Text Components")]
	[SerializeField] Text questionText;
	[Header("Positions")]
	public Transform cameraPosition;
	public Transform playerPosition;

	[SerializeField] Text[] answersTexts;


	[Space]
	[Header("Triggers")]
	[SerializeField] GameObject[] answersTriggers;

	[Space]
	[SerializeField] GameObject player;
	[SerializeField] new UnityStandardAssets.Cameras.AutoCam camera;

	[SerializeField] Transform mcqCam, mainCam;
	[SerializeField] GameObject topBar;

	// Start is called before the first frame update
	void OnEnable()
    {
		if(QuestionsManager.mcqQuestions.Count <= 0)
		{
			QuestionsManager.FindAnotherGame();
			return;
		}

		topBar.SetActive(false);
		UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl.Instance.m_Cam = mcqCam;
		question = QuestionsManager.mcqQuestions[0];
		QuestionsManager.mcqQuestions.RemoveAt(0);

		questionText.text = ArabicFixer.Fix(question.question);

		List<string> answers = question.answers;

		int rand = Random.Range(0, answersTriggers.Length);
		int trueAnswer = rand;

		for (int i = 0; i < 4; i++)
		{
			Debug.Log((i + 1) + ArabicFixer.Fix(answers[i]));
			answersTriggers[i].tag = "False";
			answersTexts[i].text = ArabicFixer.Fix(answers[i]);
		}

		//Swapping with the correct answer position, which is always 0
		answersTexts[0].text = ArabicFixer.Fix(answers[trueAnswer]);
		answersTriggers[trueAnswer].tag = "TrueMCQ";
		answersTexts[trueAnswer].text = ArabicFixer.Fix(answers[0]);
	}

	private void OnDisable()
	{
		UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl.Instance.m_Cam = mainCam;
		topBar.SetActive(true);
	}
}