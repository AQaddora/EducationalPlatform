using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using ArabicSupport;
using System.Collections.Generic;

public class AddGrade : MonoBehaviour
{
	public string addGradeUrl;
	public string newsUrl;
	public UnityEngine.UI.InputField code;

	public static NewNewsOutput[] news;
	public static string gradeName;
	public static int Grade_ID;
	public GameObject newsListingPrefab;
	public Transform newsContainer;

	private void Start()
	{
		if (PlayerPrefs.HasKey("Grade_ID"))
		{
			Login.accessToken = PlayerPrefs.GetString("accessToken");
			Login.id = PlayerPrefs.GetInt("ID");
			Grade_ID = PlayerPrefs.GetInt("Grade_ID");
			gradeName = PlayerPrefs.GetString("gradeName");
			UIManager.Instance.gradeText.text = ArabicFixer.Fix(gradeName, false, false);
			UIManager.Instance.HideAll();
			StartCoroutine(GrapNews());
			UIManager.Instance.ShowNews();
		}
		else
		{
			UIManager.Instance.ShowLogin();
		}
	}

	IEnumerator GrapNews()
	{
		//UIManager.Instance.ShowLoadingUrl();
		WWWForm form = new WWWForm();
		form.AddField("grade_id", Grade_ID);

		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);

		Debug.Log(Login.accessToken);
		WWW request = new WWW(newsUrl, form.data, headers);
		yield return request;

		Debug.Log(request.isDone + " " + request.text);
		string fixedJson = JsonHelper.FixJsonArray(request.text);
		Debug.Log(fixedJson);

		NewsOutput output = JsonUtility.FromJson<NewsOutput>(fixedJson);
		Debug.Log(output.status);
		Debug.Log(Grade_ID);
		if (output.status)
		{
			Debug.Log(output.news);
			NewNewsOutput[] _news = JsonHelper.FromJson<NewNewsOutput>(output.news);
			news = _news;
			Debug.Log(_news.Length);

			if (_news.Length == 0)
			{
				UIManager.Instance.ShowError("لا يوجد أخبار", "لم يقم معلموك بإضافة أي أخبار");
				UIManager.Instance.HideLoadingUrl();
				yield break;
			}

			for (int i = 0; i < newsContainer.childCount; i++)
			{
				Destroy(newsContainer.GetChild(i).gameObject);
			}
			for (int i = 0; i < _news.Length; i++)
			{
				GameObject obj = Instantiate(newsListingPrefab);
				obj.GetComponent<RectTransform>().SetParent(newsContainer);
				obj.GetComponent<RectTransform>().localScale = Vector3.one;
				News newNews = obj.GetComponent<News>();
				newNews.text.text = ArabicFixer.Fix(_news[i].description);
				UnityWebRequest www = UnityWebRequestTexture.GetTexture(_news[i].image_path);
				yield return www.SendWebRequest();
				Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
				newNews.image.sprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(0, 0));
			}
		}
		else
		{
			UIManager.Instance.ShowError("الأخبار", "خطأ في عملية الحصول على الأخبار");
		}
		//UIManager.Instance.HideLoadingUrl();
	}

	public void AddGrade_Button()
	{
		WWWForm form = new WWWForm();
		//form.AddField("accessToken", Login.accessToken);
		form.AddField("code", code.text);
		StartCoroutine(RegisterGrade(addGradeUrl, form));
	}

	IEnumerator RegisterGrade(string url, WWWForm form)
	{
		UIManager.Instance.ShowLoadingUrl();
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);
		
		//UnityWebRequest request = UnityWebRequest.Post(url, form);
		//request = new UnityWebRequest(url, form.data, headers);
		WWW request = new WWW(url, form.data, headers);
		yield return request;

		Debug.Log("HERE: " + request.text);

		AddGradeOutput output = JsonUtility.FromJson<AddGradeOutput>(request.text);

		if (output.status)
		{
			Grade_ID = output.@class.id;
			gradeName = output.@class.name;
			Debug.Log(output.@class.id + ": " +output.@class.name);
			UIManager.Instance.gradeText.text = ArabicFixer.Fix("الصف " + gradeName, false, false);
			PlayerPrefs.SetString("gradeName", gradeName);
			PlayerPrefs.SetInt("Grade_ID", Grade_ID);
			UIManager.Instance.HideAll();
			UIManager.Instance.ShowNews();
			StartCoroutine(GrapNews());
		}
		else
		{
			UIManager.Instance.ShowError("الانضمام للصف", "خطأ في عملية الانضمام للصف");
		}
		UIManager.Instance.HideLoadingUrl();
	}
}

[System.Serializable]
public class AddGradeOutput
{
	public bool status;
	public string msg;
	public ClassOutput @class;
}

[System.Serializable]
public class ClassOutput
{
	public int id;
	public int teacher_id;
	public string name;
	public string code;
}

[System.Serializable]
public class NewsOutput
{
	public bool status;
	public string news;
}

[System.Serializable]
public class NewNewsOutput
{
	public int id;
	public int grade_id;
	public string image_path;
	public string description;
}