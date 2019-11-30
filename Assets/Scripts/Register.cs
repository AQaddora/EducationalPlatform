using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class Register : MonoBehaviour
{
	public static Register Instance;

	public string registerUrl;
	public UnityEngine.UI.InputField full_name_input;
	public UnityEngine.UI.InputField user_name_input;
	public UnityEngine.UI.InputField password_input;

	void Start()
	{
		Instance = this;
	}

	public void RegisterNewStudent()
	{
		if(string.IsNullOrEmpty(full_name_input.text) || string.IsNullOrEmpty(user_name_input.text) || string.IsNullOrEmpty(password_input.text))
		{
			UIManager.Instance.ShowError("التسجيل", "يرجى التأكد من البيانات");
			return;
		}
		WWWForm form = new WWWForm();
		form.AddField("full_name", full_name_input.text);
		form.AddField("user_name", user_name_input.text);
		form.AddField("password", password_input.text);

		StartCoroutine(RegisterWebRequest(registerUrl, form));
	}

	IEnumerator RegisterWebRequest(string url, WWWForm form)
	{
		UIManager.Instance.ShowLoadingUrl();
		UnityWebRequest request = UnityWebRequest.Post(url, form);
		yield return request.SendWebRequest();
		Debug.Log(request.downloadHandler.text);

		if (JsonUtility.FromJson<RegisterOutput>(request.downloadHandler.text).status)
		{
			Login.accessToken = JsonUtility.FromJson<RegisterOutput>(request.downloadHandler.text).student.accessToken;
			Login.id = JsonUtility.FromJson<RegisterOutput>(request.downloadHandler.text).student.id;
			PlayerPrefs.SetString("accessToken", Login.accessToken);
			PlayerPrefs.SetInt("ID", Login.id);
			UIManager.Instance.ShowNotification("التسجيل", "تم تسجيلك بنجاح.");
			UIManager.Instance.ShowAddGrade();
			UIManager.Instance.HideLogin();
			UIManager.Instance.HideRegister();
		}
		else
		{
			UIManager.Instance.ShowError("التسجيل", "خطأ في التسجيل");
		}
		UIManager.Instance.HideLoadingUrl();
	}

}

public class StudentData
{
	public string full_name;
	public string user_name;
	public string password;

	public StudentData(string full_name, string user_name, string password)
	{
		this.user_name = user_name;
		this.full_name = full_name;
		this.password = password;
	}
}


public class RegisterOutput
{
	public bool status;
	public Student student;
	public string msg;
}