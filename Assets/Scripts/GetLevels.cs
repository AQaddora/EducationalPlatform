using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLevels : MonoBehaviour
{
	public static GetLevels Instance;

	public static string levelTitle;
	public static string videoUrl;
	public static int level_id;

	public string questionsUrl;
	public string levelsUrl;
	public GameObject levelListingPrefab;
	public Transform levelsContainer;

	public static int QuestionsCount;
	public static List<Match> matchQuestions;
	public static List<MCQ> mcqQuestions;
	public static List<TrueOrFalse> tOrFQuestions;

	private void Awake()
	{
		Instance = this;
	}

	public void OnClickButton()
	{
		WWWForm form = new WWWForm();
		form.AddField("subject_id", GetSubjects.subject_id);
		StartCoroutine(GetLevelsFromSubject(levelsUrl, form));
	}

	public static void OnClickButton_Level(string videoUrl, string title)
	{
		GetLevels.levelTitle = title;
		GetLevels.videoUrl = videoUrl;
		//Debug.Log(GetLevels.videoUrl);
	}

	public IEnumerator GetQuestions(int level_id)
	{
		UIManager.Instance.ShowLoadingUrl();
		QuestionsManager.level_id = level_id;
		QuestionsManager.student_id = Login.id;
		Debug.Log("HERE         SADFASDF ASDF ASD FASFD  "+ level_id + " " + Login.id);
		WWWForm form = new WWWForm();
		form.AddField("level_id", level_id);

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);

		Debug.Log(Login.accessToken);
		WWW request = new WWW(questionsUrl, form.data, headers);
		yield return request;

		//string fixedJson = JsonHelper.FixJsonArray(request.text);

		Debug.Log(request.text);
		QuestionLevelOutput output = JsonUtility.FromJson<QuestionLevelOutput>(request.text);

		if (output.status)
		{
			mcqQuestions = output.MCQ;
			tOrFQuestions = output.TrueOrFalse;
			matchQuestions = output.match;

			QuestionsCount = mcqQuestions.Count + matchQuestions.Count + tOrFQuestions.Count;
			Debug.Log(mcqQuestions[0].question);
			if (QuestionsCount < 5)
			{
				UIManager.Instance.ShowError("لا يوجد أسئلة", "لم يقم معلموك بإضافة أسئلة كافية، عد لاحقا!");
			}
			else
			{
				Destroy(UIManager.Instance.loadingUrl.gameObject);
				loadingSceneFader.cg.alpha = 1;
				StartCoroutine(AsynchronousLoad(1));
			}

		}

		yield return null;
	}

	AsyncOperation asyncLoad;
	public Fader loadingSceneFader;
	public UnityEngine.UI.Slider loading;
	public UnityEngine.UI.Text loadingText;

	IEnumerator AsynchronousLoad(int sceneIndex)
	{
		yield return null;

		AsyncOperation ao = SceneManager.LoadSceneAsync(sceneIndex);

		while (!ao.isDone)
		{
			float progress = Mathf.Clamp01(ao.progress / 0.9f);
			if (loading.value < progress)
			{
				loading.value += Time.deltaTime * 5;
				loadingText.text = (loading.value * 100).ToString("F0") + "%";
			}
			yield return null;
		}
	}

	IEnumerator GetLevelsFromSubject(string url, WWWForm form)
	{
		UIManager.Instance.ShowLoadingUrl();

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);

		Debug.Log(Login.accessToken);

		WWW request = new WWW(url, form.data, headers);
		yield return request;

		string fixedJson = JsonHelper.FixJsonArray(request.text);

		Debug.Log(fixedJson);
		SubjectLevelOutput output = JsonUtility.FromJson<SubjectLevelOutput>(fixedJson);

		if (output.status)
		{
			Level[] _levels = JsonHelper.FromJson<Level>(output.levels);

			for (int i = 0; i < levelsContainer.childCount; i++)
			{
				Destroy(levelsContainer.GetChild(i).gameObject);
			}

			for (int i = 0; i < _levels.Length; i++)
			{
				GameObject obj = Instantiate(levelListingPrefab);
				obj.GetComponent<RectTransform>().SetParent(levelsContainer);
				obj.GetComponent<RectTransform>().localScale = Vector3.one;
				obj.GetComponent<LevelItem>().levelTitle = _levels[i].title;
				obj.GetComponent<LevelItem>().videoUrl = _levels[i].video_level_path;
				obj.GetComponent<LevelItem>().levelId = _levels[i].id;
				obj.GetComponent<LevelItem>().isAvailable = _levels[i].isAvilable == 1;
				obj.GetComponent<LevelItem>().SetItemStuff();

			}
		}
		UIManager.Instance.HideLoadingUrl();
	}
}

[System.Serializable]
public class SubjectLevelOutput
{
	public bool status;
	public string levels;
}

[System.Serializable]
public class QuestionLevelOutput
{
	public bool status;
	public string msg;

	public List<TrueOrFalse> TrueOrFalse;
	public List<Match> match;
	public List<MCQ> MCQ;
}

[System.Serializable]
public class Level
{
	public int id;
	public string title;
	public string video_level_path;
	public int isAvilable;
}

[System.Serializable]
public class TrueOrFalse
{
	public int id;
	public int type_id;
	public string question;
	public string answers;
}
[System.Serializable]
public class MCQ
{
	public int id;
	public int type_id;
	public string question;
	public List<string> answers;
	public int has_image;
	public string question_image_path;
}

[System.Serializable]
public class Match
{
	public int id;
	public int type_id;
	public string question;
	public string answers;
}


