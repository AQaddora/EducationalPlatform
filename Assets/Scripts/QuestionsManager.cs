using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
	[Space]
	public static QuestionsManager Instance;
	public static List<Match> matchQuestions;
	public static List<TrueOrFalse> tfQuestions;
	public static List<MCQ> mcqQuestions;
	public static int level_id;
	public static int student_id;

	public GameObject trueOrFalse, mcqGame, matchGame;
	public UIAlpha blackPanel;
	[Space]
	[Header("Report Grade")]
	public string postGradeUrl;
	void Awake()
    {
		Instance = this;
		matchQuestions = GetLevels.matchQuestions;
		tfQuestions = GetLevels.tOrFQuestions;
		mcqQuestions = GetLevels.mcqQuestions;
	}

	public void ClearAllQuestionGames()
	{
		blackPanel.FadeInOut();
		Invoke("SetThemNotActive", blackPanel.duration);
	}
	void SetThemNotActive()
	{
		trueOrFalse.SetActive(false);
		matchGame.SetActive(false);
		mcqGame.SetActive(false);
		SceneManager_Script.Instance.MovePlayerRandomly();
	}

	public static void FindAnotherGame()
	{
		Instance.SetThemNotActive();
		if(tfQuestions.Count >= 3)
		{
			Instance.trueOrFalse.SetActive(true);
		}/*else if(matchQuestions.Count > /*MATCH POLES0)
		{
			Instance.matchGame.SetActive(true);
		}*/else if(mcqQuestions.Count > 0)
		{
			Instance.mcqGame.SetActive(true);
		}
		else
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}
	}

	public IEnumerator ReportQuestionsStatus(int question_id, int grade)
	{
		WWWForm form = new WWWForm();
		form.AddField("student_id", Login.id);
		form.AddField("level_id", GetLevels.level_id);
		form.AddField("question_id", question_id);
		form.AddField("grade", grade);

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);
		Debug.Log(Login.id + ", " + GetLevels.level_id + ", " + question_id + ", " + grade);
		
		WWW request = new WWW(postGradeUrl, form.data, headers);
		yield return request;
		Debug.Log("HERE: " + request.text);
	}
}
