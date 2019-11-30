using ArabicSupport;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetSubjects : MonoBehaviour
{

	public string subjectsUrl;
	public static int subject_id;

	public static void OnButtonClick(int subject_id)
	{
		GetSubjects.subject_id = subject_id;
		Debug.Log("Chosen: " + subject_id);
		GetLevels.Instance.OnClickButton();
	}

	public GameObject subjectListingPrefab;
	public Transform subjectsContainer;

	public void GetSubjectsOnClick()
	{
		WWWForm form = new WWWForm();
		form.AddField("grade_id", AddGrade.Grade_ID);
		StartCoroutine(GetSubjectsForGrade(subjectsUrl, form));
	}
	
	IEnumerator GetSubjectsForGrade(string url, WWWForm form)
	{
		UIManager.Instance.ShowLoadingUrl();
		//UIManager.Instance.ShowCourses();
		Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Authorization", "Bearer " + Login.accessToken);

		Debug.Log(Login.accessToken);
		//UnityWebRequest request = UnityWebRequest.Post(url, form);
		//request = new UnityWebRequest(url, form.data, headers);
		WWW request = new WWW(url, form.data, headers);
		yield return request;

		string fixedJson = JsonHelper.FixJsonArray(request.text);
		Debug.Log(fixedJson);

		GradesSubjectsOutput output = JsonUtility.FromJson<GradesSubjectsOutput>(fixedJson);

		if (output.status)
		{
			Subject[] _subjects = JsonHelper.FromJson<Subject>(output.subjects);

			for (int i = 0; i < subjectsContainer.childCount; i++)
			{
				Destroy(subjectsContainer.GetChild(i).gameObject);
			}

			for (int i = 0; i < _subjects.Length; i++)
			{
				GameObject obj = Instantiate(subjectListingPrefab);
				obj.GetComponent<RectTransform>().SetParent(subjectsContainer);
				obj.GetComponent<RectTransform>().localScale = Vector3.one;
				obj.GetComponent<SubjectItem>().text.text = ArabicFixer.Fix(_subjects[i].name);
				obj.GetComponent<SubjectItem>().subject_id = _subjects[i].id;
				obj.GetComponent<SubjectItem>().subjectName = _subjects[i].name;
			}
			UIManager.Instance.HideAll();
			UIManager.Instance.ShowCourses();
		}
		UIManager.Instance.HideLoadingUrl();
	}
}

[System.Serializable]
public class GradesSubjectsOutput
{
	public bool status;
	public string subjects;
}

[System.Serializable]
public class Subject
{
	public int id;
	public string name;
}
